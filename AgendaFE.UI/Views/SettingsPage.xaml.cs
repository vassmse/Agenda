using AgendaContracts.Models;
using AgendaFE.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaFE.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public SettingsPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
        }

        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            CategoryDto source = ((Button)sender).DataContext as CategoryDto;
            ViewModel.DeleteCategoryAction(source);
        }

        private void RenameCategory(object sender, RoutedEventArgs e)
        {            
            CategoryDto source = ((Button)sender).DataContext as CategoryDto;
            ViewModel.RenameCategoryAction(source);
        }

        //private void EnterPressed(object sender, KeyRoutedEventArgs e)
        //{
        //    if(e.Key == Windows.System.VirtualKey.Enter)
        //    {
        //        CategoryDto source = ((TextBox)sender).DataContext as CategoryDto;
        //        ViewModel.RenameCategoryAction(source);
        //    }
        //}

        private void RenameByClick(object sender, DoubleTappedRoutedEventArgs e)
        {
            CategoryDto source = ((TextBlock)sender).DataContext as CategoryDto;
            ViewModel.RenameCategoryAction(source);
        }
    }
}
