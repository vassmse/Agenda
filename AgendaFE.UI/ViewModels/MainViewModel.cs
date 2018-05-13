using AgendaContracts.Models;
using AgendaFE.UI.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgendaFE.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Command properties

        public RelayCommand AddCategoryCommand { get; private set; }

        public RelayCommand AddTaskCommand { get; private set; }
        public RelayCommand<int> AddSubTaskCommand { get; private set; }

        public RelayCommand<int> SelectedTaskCommand { get; private set; }

        public RelayCommand SaveTaskCommand { get; private set; }

        public RelayCommand DeleteTaskCommand { get; private set; }

        public RelayCommand<CategoryDto> DeleteCategoryCommand { get; private set; }

        public RelayCommand<CategoryDto> RenameCategoryCommand { get; private set; }


        #endregion


        #region Full Properties

        private TaskDto selectedTask;

        public TaskDto SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                RaisePropertyChanged("SelectedTask");
            }
        }


        private bool isPanelActive;

        public bool IsPanelActive
        {
            get { return isPanelActive; }
            set
            {
                isPanelActive = value;
                RaisePropertyChanged(nameof(IsPanelActive));
            }
        }

        private bool hasDeadlineDate;

        public bool HasDeadlineDate
        {
            get { return hasDeadlineDate; }
            set
            {
                hasDeadlineDate = value;
                RaisePropertyChanged(nameof(HasDeadlineDate));
            }
        }

        private ObservableCollection<CategoryDto> categories;

        public ObservableCollection<CategoryDto> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }

        private ObservableCollection<TaskDto> allTasks;

        public ObservableCollection<TaskDto> AllTasks

        {
            get { return allTasks; }
            set
            {
                allTasks = value;
                RaisePropertyChanged(nameof(AllTasks));
            }
        }


        public ObservableCollection<TaskDto> DailyTasks
        {
            get { return new ObservableCollection<TaskDto>(AllTasks.Where(t => (!t.IsSubTask) && ( (t.HasDeadlineDate && t.DeadlineDate.Year == DateTime.Now.Year && t.DeadlineDate.Day == DateTime.Now.Day) || (t.HasScheduledDate && t.ScheduledDate.Year == DateTime.Now.Year && t.ScheduledDate.Day == DateTime.Now.Day))).ToList()); }
        }

        public ObservableCollection<TaskDto> WeeklyTasks
        {
            get { return new ObservableCollection<TaskDto>(AllTasks.Where(t => (!t.IsSubTask) && ((t.HasDeadlineDate && t.DeadlineDate.DayOfYear < DateTime.Now.DayOfYear + 7 && t.DeadlineDate.DayOfYear >= DateTime.Now.DayOfYear) || (t.HasScheduledDate && t.ScheduledDate.DayOfYear < DateTime.Now.DayOfYear + 7 && t.ScheduledDate.DayOfYear >= DateTime.Now.DayOfYear))).ToList()); }
        }

        public ObservableCollection<TaskDto> ExpiredTasks
        {
            get { return new ObservableCollection<TaskDto>(AllTasks.Where(t => (!t.IsSubTask) && (t.HasDeadlineDate && t.DeadlineDate.Day < DateTime.Now.Day)).ToList()); }
        }


        private CategoryDto newCategory;

        public CategoryDto NewCategory
        {
            get { return newCategory; }
            set
            {
                newCategory = value;
                RaisePropertyChanged(nameof(NewCategory));
            }
        }

        private CategoryDto selectedCategory;

        public CategoryDto SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                IsPanelActive = false;
                selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }




        #endregion

        #region Private properties        

        private AgendaRestClient RestClient { get; set; }

        #endregion

        public MainViewModel()
        {
            #region RelayCommands

            AddTaskCommand = new RelayCommand(AddTaskAction);
            AddCategoryCommand = new RelayCommand(AddCategoryAction);
            SelectedTaskCommand = new RelayCommand<int>(SelectedTaskAction);
            SaveTaskCommand = new RelayCommand(SaveTaskAction);
            DeleteTaskCommand = new RelayCommand(DeleteTaskAction);
            DeleteCategoryCommand = new RelayCommand<CategoryDto>(DeleteCategoryAction);
            RenameCategoryCommand = new RelayCommand<CategoryDto>(RenameCategoryAction);
            AddSubTaskCommand = new RelayCommand<int>(AddSubTaskAction);



            #endregion

            RestClient = new AgendaRestClient();
            Categories = new ObservableCollection<CategoryDto>(RestClient.GetCategories());
            NewCategory = new CategoryDto();
            SelectedCategory = new CategoryDto();
            SelectedTask = new TaskDto();
            AllTasks = new ObservableCollection<TaskDto>(RestClient.GetAllTasks());

            foreach (var category in Categories)
            {
                foreach (var task in AllTasks)
                {
                    if (category.Id == task.ParentId)
                    {
                        category.Tasks.Add(task);
                    }
                }
            }


        }

        public void ChangeTaskState(int taskId, int newState)
        {
            var task = AllTasks.Where(t => t.Id == taskId).First();
            var selectedTask = SelectedCategory.Tasks.Where(t => t.Id == taskId).First();
            task.State = newState;
            selectedTask.State = newState;
            SelectedCategory.NotifyProperty();
            RestClient.UpdateTask(task);
        }


        #region Commands

        public void AddCategoryAction()
        {
            int id = Categories.Last().Id + 1;
            var newCategory = new CategoryDto { Id = id, Name = NewCategory.Name, Description = NewCategory.Description, StateType = NewCategory.StateType };
            RestClient.AddCategory(newCategory);
            Categories.Add(newCategory);
            NavigationViewItems.AddMenuItem(newCategory);

            //Add dummy tasks
            int taskId = AllTasks.Last().Id + 1;
            var newTask = new TaskDto { Id = taskId, Name = "TO-DO 1", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = newCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == newCategory.Id).First().Tasks.Add(newTask);

            taskId = AllTasks.Last().Id + 1;
            newTask = new TaskDto { Id = taskId, Name = "TO-DO 2", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = newCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == newCategory.Id).First().Tasks.Add(newTask);

            taskId = AllTasks.Last().Id + 1;
            newTask = new TaskDto { Id = taskId, Name = "TO-DO 3", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = newCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == newCategory.Id).First().Tasks.Add(newTask);


            NewCategory.Name = String.Empty;
            NewCategory.Description = String.Empty;
        }

        public void AddTaskAction()
        {
            int id = AllTasks.Last().Id + 1;
            var newTask = new TaskDto { Id = id, Name = "New Task", Description = "", State = 3, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = SelectedCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            // Categories.Where(n => n.Id == SelectedCategory.Id).First().Tasks.Add(newTask);
            SelectedCategory.Tasks.Add(newTask);
            SelectedCategory.NotifyProperty();
        }

        public void AddSubTaskAction(int parentId)
        {
            int id = AllTasks.Last().Id + 1;
            var newTask = new TaskDto { Id = id, Name = "New SubTask", Description = "", State = 3, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = SelectedCategory.Id, ParentTaskId = parentId, IsSubTask=true };
            //TODO:restclient
            SelectedCategory.Tasks.Where(t => t.Id == parentId).First().SubTasks.Add(newTask);
            SelectedCategory.NotifyProperty();
            AllTasks.Add(newTask);
        }

        public void SelectedTaskAction(int taskId)
        {
            if (SelectedTask.Id == taskId)
                IsPanelActive = !IsPanelActive;
            else
            {
                SelectedTask = SelectedCategory.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
                if (SelectedTask == null)
                {
                    foreach (var task in SelectedCategory.Tasks)
                    {
                        SelectedTask = task.SubTasks.Where(t => t.Id == taskId).FirstOrDefault();
                        if (SelectedTask != null)
                            break;
                    }
                }
                IsPanelActive = true;
            }
        }

        public void SaveTaskAction()
        {
            RestClient.UpdateTask(SelectedTask);

            for (int i = 0; i < Categories.Count(); i++)
            {
                if (Categories[i] == SelectedCategory)
                {
                    for (int j = 0; j < Categories[i].Tasks.Count(); j++)
                    {
                        if (Categories[i].Tasks[j].Id == SelectedTask.Id)
                        {
                            Categories[i].Tasks[j] = SelectedTask;
                        }
                        else
                        {
                            for (int k = 0; k < Categories[i].Tasks[j].SubTasks.Count(); k++)
                            {
                                if (Categories[i].Tasks[j].SubTasks[k].Id == SelectedTask.Id)
                                {
                                    Categories[i].Tasks[j].SubTasks[k] = SelectedTask;
                                }
                            }
                        }
                    }
                }
            }

            SelectedCategory.NotifyProperty();

        }

        public void DeleteTaskAction()
        {
            RestClient.DeleteTask(SelectedTask);
            IsPanelActive = false;
            AllTasks.Remove(SelectedTask);

            for (int i = 0; i < Categories.Count(); i++)
            {
                if (Categories[i] == SelectedCategory)
                {
                    for (int j = 0; j < Categories[i].Tasks.Count(); j++)
                    {
                        if (Categories[i].Tasks[j].Id == SelectedTask.Id)
                        {
                            Categories[i].Tasks.RemoveAt(j);

                        }
                    }
                }
            }

            SelectedCategory.NotifyProperty();

        }

        public void CheckChangedAction(TaskDto task)
        {
            RestClient.UpdateTask(task);
        }

        public void DeleteCategoryAction(CategoryDto category)
        {
            RestClient.DeleteCategory(category);
            Categories.Remove(category);
            NavigationViewItems.DeleteMenuItem(category.Name);
        }

        public void SaveCategoryAction(CategoryDto category)
        {
            RestClient.UpdateCategory(category);
            NavigationViewItems.ChangeTag(category);
        }

        public void RenameCategoryAction(CategoryDto category)
        {
            if (!category.Renaming)
            {
                category.Renaming = true;
                category.OldName = category.Name;
            }
            else
            {
                category.Renaming = false;
                NavigationViewItems.RenameMenuItem(category.OldName, category.Name);
                RestClient.UpdateCategory(category);
            }
        }

        public void HideCategory(bool hide, string name)
        {
            NavigationViewItems.HideMenuItem(hide, name);
        }

        #endregion

    }
}
