using AgendaFE.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AgendaFE.UI.Models
{
    public class NavigationViewItems : INotifyPropertyChanged
    {
        public static ObservableCollection<NavigationViewItemBase> menu = new ObservableCollection<NavigationViewItemBase>();

        private static MainViewModel ViewModel;

        private static ViewModelLocator vm = new ViewModelLocator();

        static NavigationViewItems()
        {
            ViewModel = vm.MainPage;
            menu.Add(new NavigationViewItemHeader { Content = "Calendar", Margin = new Thickness(33, 0, 0, 0) });
            menu.Add(new NavigationViewItem() { Content = "Today", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "today" });
            menu.Add(new NavigationViewItem() { Content = "7 days", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "week" });
            menu.Add(new NavigationViewItem() { Content = "Expired", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "expired" });
            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItemHeader { Content = "Categories", Margin = new Thickness(33, 0, 0, 0) });

            foreach (var category in ViewModel.Categories)
            {
                menu.Add(new NavigationViewItem { Content = category.Name, Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            }

            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public static void AddMenuItem(string name)
        {
            menu.RemoveAt(menu.Count - 1);
            menu.RemoveAt(menu.Count - 1);
            menu.Add(new NavigationViewItem { Content = name, Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public static void DeleteMenuItem(string name)
        {
            NavigationViewItemBase deletion = null;
            foreach(var item in menu)
            {
                if (item.Content!=null && item.Content.ToString() == name)
                    deletion = item;
            }
            menu.Remove(deletion);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
