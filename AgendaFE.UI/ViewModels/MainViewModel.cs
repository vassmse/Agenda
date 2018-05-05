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


        public List<TaskDto> DailyTasks
        {
            get { return AllTasks.Where(t => (t.HasDeadlineDate && t.DeadlineDate.Year == DateTime.Now.Year && t.DeadlineDate.Day == DateTime.Now.Day) || (t.HasScheduledDate && t.ScheduledDate.Year == DateTime.Now.Year && t.ScheduledDate.Day == DateTime.Now.Day)).ToList(); }
        }

        public List<TaskDto> WeeklyTasks
        {
            get { return AllTasks.Where(t => (t.HasDeadlineDate && t.DeadlineDate.DayOfYear < DateTime.Now.DayOfYear + 7 && t.DeadlineDate.DayOfYear >= DateTime.Now.DayOfYear) || (t.HasScheduledDate && t.ScheduledDate.DayOfYear < DateTime.Now.DayOfYear + 7 && t.ScheduledDate.DayOfYear >= DateTime.Now.DayOfYear)).ToList(); }
        }

        public List<TaskDto> ExpiredTasks
        {
            get { return AllTasks.Where(t => t.HasDeadlineDate && t.DeadlineDate.Day < DateTime.Now.Day).ToList(); }
        }

        public List<TaskDto> ToDoTasks
        {
            get { return AllTasks.Where(t => t.State==0).ToList(); }
        }

        public List<TaskDto> DoingTasks
        {
            get { return AllTasks.Where(t => t.State == 2).ToList(); }
        }

        public List<TaskDto> DoneTasks
        {
            get { return AllTasks.Where(t => t.State == 1).ToList(); }
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



            #endregion

            RestClient = new AgendaRestClient();
            Categories = new ObservableCollection<CategoryDto>(RestClient.GetCategories());
            NewCategory = new CategoryDto();
            SelectedCategory = new CategoryDto();
            SelectedTask = new TaskDto();
            AllTasks = new ObservableCollection<TaskDto>(RestClient.GetAllTasks());

            //Dummy datas


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


        #region Commands

        public void AddCategoryAction()
        {
            int id = Categories.Last().Id + 1;
            var newCategory = new CategoryDto { Id = id, Name = NewCategory.Name, Description = NewCategory.Description, StateType = NewCategory.StateType };
            RestClient.AddCategory(newCategory);
            Categories.Add(newCategory);
            NavigationViewItems.AddMenuItem(newCategory.Name);

            //Add dummy tasks
            int taskId = AllTasks.Last().Id + 1;
            var newTask = new TaskDto { Id = id, Name = "TO-DO 1", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = newCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == newCategory.Id).First().Tasks.Add(newTask);

            taskId = AllTasks.Last().Id + 1;
            newTask = new TaskDto { Id = id, Name = "TO-DO 2", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = newCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == newCategory.Id).First().Tasks.Add(newTask);

            taskId = AllTasks.Last().Id + 1;
            newTask = new TaskDto { Id = id, Name = "TO-DO 3", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = newCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == newCategory.Id).First().Tasks.Add(newTask);


            NewCategory.Name = String.Empty;
            NewCategory.Description = String.Empty;
        }

        public void AddTaskAction()
        {
            int id = AllTasks.Last().Id + 1;
            var newTask = new TaskDto { Id = id, Name = "New Task", Description = "", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = SelectedCategory.Id };
            RestClient.AddTask(newTask);
            AllTasks.Add(newTask);
            Categories.Where(n => n.Id == SelectedCategory.Id).First().Tasks.Add(newTask);
        }

        public void SelectedTaskAction(int taskId)
        {
            if (SelectedTask.Id == taskId)
                IsPanelActive = !IsPanelActive;
            else
            {
                SelectedTask = SelectedCategory.Tasks.Where(t => t.Id == taskId).First();
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
                        if (Categories[i].Tasks[j].Name == SelectedTask.Name)
                        {
                            Categories[i].Tasks[j] = SelectedTask;
                        }
                    }
                }
            }
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
                        if (Categories[i].Tasks[j].Name == SelectedTask.Name)
                        {
                            Categories[i].Tasks.RemoveAt(j);

                        }
                    }
                }
            }
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
