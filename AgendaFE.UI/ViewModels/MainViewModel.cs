﻿using AgendaContracts.Models;
using AgendaFE.Logic.EntryPoints;
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

        #endregion

        #region Full Properties

        public string TestString { get; set; }

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

        private List<string> testList;

        public List<string> TestList
        {
            get { return testList; }
            set { testList = value; }
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

        private PresentationManager BusinessLayer { get; set; }

        #endregion

        public MainViewModel()
        {
            #region RelayCommands

            AddTaskCommand = new RelayCommand(AddTask);
            AddCategoryCommand = new RelayCommand(AddCategory);
          
            #endregion

            BusinessLayer = new PresentationManager();
            Categories = new ObservableCollection<CategoryDto>(BusinessLayer.GetCategories());            
            NewCategory = new CategoryDto();
            SelectedCategory = new CategoryDto();
            TestString = "TestString";

            //Dummy datas
            SelectedCategory.Name = "School";
            SelectedCategory.StateType = StateTypes.Checklist;
            SelectedCategory.Tasks = new ObservableCollection<TaskDto>
            {
                new TaskDto { Name = "Banana", Description = "Nice 2 bananas", State = 0, DeadlineDate = DateTime.Now },
                new TaskDto { Name = "Bread", Description = "White one", State = 0, DeadlineDate = DateTime.Now },
                new TaskDto { Name = "Water", Description = "12 bottles", State = 0, DeadlineDate = DateTime.Now }
            };

            TestList = new List<string>
            {
                "alma",
                "körte",
                "barack"
            };
        }


        #region Commands

        public void AddCategory()
        {
            //BusinessLayer.AddCategory(NewCategory);
            Categories.Add(NewCategory);
            NewCategory.Name = String.Empty;
            NewCategory.Description = String.Empty;
        }

        public void AddTask()
        {            
            SelectedCategory.Tasks.Add(new TaskDto { Name = "NewItem", Description = "--", State = 0 });
        }

        #endregion
    }
}
