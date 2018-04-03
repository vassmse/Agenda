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



        private ObservableCollection<CategoryDto> categories;

        public ObservableCollection<CategoryDto> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                RaisePropertyChanged(nameof(Categories));
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


            #endregion
            
            RestClient = new AgendaRestClient();
            Categories = new ObservableCollection<CategoryDto>(RestClient.GetCategories());
            NewCategory = new CategoryDto();
            SelectedCategory = new CategoryDto();
            SelectedTask = new TaskDto();

            //Dummy datas

            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<TaskDto>
                {
                     new TaskDto { Name = "Banana", Description = "Nice 2 bananas", State = 1, DeadlineDate = DateTime.Now },
                     new TaskDto { Name = "Bread", Description = "White one", State = 0, DeadlineDate = DateTime.Now },
                     new TaskDto { Name = "Water", Description = "12 bottles", State = 1, DeadlineDate = DateTime.Now }
                };
            }
        }


        #region Commands

        public void AddCategoryAction()
        {
            RestClient.AddCategory(NewCategory);
            Categories.Add(NewCategory);
            NavigationViewItems.AddMenuItem(NewCategory.Name);
            NewCategory.Name = String.Empty;
            NewCategory.Description = String.Empty;
        }

        public void AddTaskAction()
        {
            SelectedCategory.Tasks.Add(new TaskDto { Name = "NewItem", Description = "--", State = 0 });
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

        public void SaveTaskAction(string taskName)
        {
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

        #endregion

    }
}
