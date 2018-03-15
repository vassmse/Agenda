using AgendaFE.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AgendaFE.UI.Models
{
    public class NavigationViewItems: INotifyPropertyChanged
    {
        public static ObservableCollection<NavigationViewItemBase> menu = new ObservableCollection<NavigationViewItemBase>();

        private static MainViewModel ViewModel = new MainViewModel();

        static NavigationViewItems()
        {
            menu.Add(new NavigationViewItemHeader { Content = "Calendar" });
            menu.Add(new NavigationViewItem() { Content = "Today", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            menu.Add(new NavigationViewItem() { Content = "7 days", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            menu.Add(new NavigationViewItem() { Content = "expired", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItemHeader { Content = "Categories" });

            foreach (var category in ViewModel.Categories)
            {
                menu.Add(new NavigationViewItem { Content = category.Name, Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            }

            menu.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public static void AddMenuItem(string name)
        {
            menu.RemoveAt(menu.Count - 1);
            menu.Add(new NavigationViewItem { Content = name, Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "category" });
            menu.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
