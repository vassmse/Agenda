using AgendaContracts.Models;
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

        #endregion

        #region Full Properties

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

        private List<string> testString;

        public List<string> TestString
        {
            get { return testString; }
            set
            {
                testString = value;
                RaisePropertyChanged(nameof(TestString));
            }
        }




        #endregion

        #region Private properties

        private PresentationManager businessLayer { get; set; }

        #endregion

        public MainViewModel()
        {
            businessLayer = new PresentationManager();

            Categories = new ObservableCollection<CategoryDto>(businessLayer.GetCategories());
            NewCategory = new CategoryDto();
            AddCategoryCommand = new RelayCommand(AddCategory, CanAddCategory);
            SelectedCategory = new CategoryDto();
            //Dummy datas
            SelectedCategory.Name = "School";
            SelectedCategory.StateType = StateTypes.Checklist;
            SelectedCategory.Tasks = new List<TaskDto>
            {
                new TaskDto { Name = "Banana", Description = "Nice 2 bananas", State = 0, DeadlineDate = DateTime.Now },
                new TaskDto { Name = "Bread", Description = "White one", State = 0, DeadlineDate = DateTime.Now }
            };

            TestString = new List<string>
            {
                "alma",
                "körte",
                "banán"
            };

        }

        public bool CanAddCategory()
        {
            return true;
        }

        public void AddCategory()
        {
            //businessLayer.AddCategory(NewCategory);
            //Categories.Add(NewCategory);
            NewCategory.Name = String.Empty;
            NewCategory.Description = String.Empty;
        }
    }
}
