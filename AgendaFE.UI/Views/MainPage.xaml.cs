using AgendaFE.UI.Models;
using AgendaFE.UI.ViewModels;
using AgendaFE.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AgendaFE.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public ObservableCollection<NavigationViewItemBase> MenuItems;

        public MainPage()
        {
            InitializeComponent();
            ViewModel = vm.MainPage;
            DataContext = ViewModel;
            MenuItems = NavigationViewItems.menu;
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "today")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem);
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            NavView_Navigate(item);
        }

        private void NavView_Navigate(NavigationViewItem item)
        {

            if (item != null)
            {
                switch (item.Tag)
                {
                    case "today":
                        ContentFrame.Navigate(typeof(DailyReportPage));
                        NavView.Header = "Daily report";
                        break;
                    case "week":
                        ContentFrame.Navigate(typeof(WeeklyReportPage));
                        NavView.Header = "Weekly report";
                        break;
                    case "expired":
                        ContentFrame.Navigate(typeof(WeeklyReportPage));
                        NavView.Header = "Expired tasks";
                        break;
                    case "category":
                        ContentFrame.Navigate(typeof(SingleCategoryPage));
                        NavView.Header = item.Content;
                        var a = ViewModel.Categories;
                        if (ViewModel.Categories.FirstOrDefault(c => c.Name == item.Content.ToString()) != null)
                        {
                            ViewModel.SelectedCategory = ViewModel.Categories.FirstOrDefault(c => c.Name == item.Content.ToString()); //TODO
                        }
                        break;
                    case "addnew":
                        ContentFrame.Navigate(typeof(NewCategoryPage));
                        NavView.Header = "Add new category";
                        break;

                }
            }
        }
    }
}
