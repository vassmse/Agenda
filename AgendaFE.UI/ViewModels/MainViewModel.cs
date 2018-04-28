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

        public RelayCommand<string> SelectedTaskCommand { get; private set; }

        public RelayCommand<string> SaveTaskCommand { get; private set; }

        public RelayCommand<string> DeleteTaskCommand { get; private set; }

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
            SelectedTaskCommand = new RelayCommand<string>(SelectedTaskAction);
            SaveTaskCommand = new RelayCommand<string>(SaveTaskAction);
            DeleteTaskCommand = new RelayCommand<string>(DeleteTaskAction);
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
            RestClient.AddCategory(NewCategory);
            Categories.Add(NewCategory);
            NavigationViewItems.AddMenuItem(NewCategory.Name);
            //NewCategory.Name = "a";
            //NewCategory.Description = "a";
        }

        public void AddTaskAction()
        {
            var newTask = new TaskDto { Name = "NewItem", Description = "--", State = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId = SelectedCategory.Id };
            RestClient.AddTask(newTask);
            SelectedCategory.Tasks.Add(newTask);
            AllTasks.Add(newTask);
        }

        public void SelectedTaskAction(string taskName)
        {
            if (SelectedTask.Name == taskName)
                IsPanelActive = !IsPanelActive;
            else
            {
                SelectedTask = SelectedCategory.Tasks.Where(t => t.Name == taskName).First();
                IsPanelActive = true;
            }
        }

        public void SaveTaskAction(string taskId)
        {
            RestClient.UpdateTask(SelectedTask);

            //TODO
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

        public void DeleteTaskAction(string taskName)
        {
            RestClient.DeleteTask(SelectedTask);
            IsPanelActive = false;

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
