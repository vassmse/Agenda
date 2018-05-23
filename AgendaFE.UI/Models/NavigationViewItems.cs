using AgendaContracts.Models;
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
            menu.Add(new NavigationViewItemHeader { Content = "Naptár", Margin = new Thickness(33, 0, 0, 0) });
            menu.Add(new NavigationViewItem() { Content = "Mai nap", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "today" });
            menu.Add(new NavigationViewItem() { Content = "Egy hét", Icon = new SymbolIcon(Symbol.CalendarWeek), Tag = "week" });
            menu.Add(new NavigationViewItem() { Content = "Lejárt", Icon = new SymbolIcon(Symbol.CalendarReply), Tag = "expired" });
            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItemHeader { Content = "Kategóriák", Margin = new Thickness(33, 0, 0, 0) });

            foreach (var category in ViewModel.Categories)
            {
                SymbolIcon icon = new SymbolIcon();
                switch (category.StateType)
                {
                    case StateTypes.Checklist:
                        icon = new SymbolIcon(Symbol.AllApps);
                        break;
                    case StateTypes.Kanban3:
                        icon = new SymbolIcon(Symbol.DockBottom);
                        break;
                    case StateTypes.Kanban5:
                        icon = new SymbolIcon(Symbol.SelectAll);
                        break;
                    case StateTypes.MultiChecklist:
                        icon = new SymbolIcon(Symbol.Bookmarks);
                        break;
                }
                menu.Add(new NavigationViewItem { Content = category.Name, Icon = icon, Tag = category.StateType.ToString() });                
            }

            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItem { Content = "Új kategória hozzáadása", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });

        }

        public static void AddMenuItem(CategoryDto category)
        {
            SymbolIcon icon = new SymbolIcon();
            switch (category.StateType)
            {
                case StateTypes.Checklist:
                    icon = new SymbolIcon(Symbol.AllApps);
                    break;
                case StateTypes.Kanban3:
                    icon = new SymbolIcon(Symbol.DockBottom);
                    break;
                case StateTypes.Kanban5:
                    icon = new SymbolIcon(Symbol.SelectAll);
                    break;
                case StateTypes.MultiChecklist:
                    icon = new SymbolIcon(Symbol.Bookmarks);
                    break;
            }

            menu.RemoveAt(menu.Count - 1);
            menu.RemoveAt(menu.Count - 1);
            menu.Add(new NavigationViewItem { Content = category.Name, Icon = icon, Tag = category.StateType.ToString() });
            menu.Add(new NavigationViewItemSeparator());
            menu.Add(new NavigationViewItem { Content = "Új kategória hozzáadása", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public static void DeleteMenuItem(string name)
        {
            NavigationViewItemBase deletion = null;
            foreach (var item in menu)
            {
                if (item.Content != null && item.Content.ToString() == name)
                    deletion = item;
            }
            menu.Remove(deletion);
        }

        public static void RenameMenuItem(string oldName, string newName)
        {
            foreach (var item in menu)
            {
                if (item.Content != null && item.Content.ToString() == oldName)
                    item.Content = newName;
            }
        }

        public static void HideMenuItem(bool hide, string name)
        {
            foreach (var item in menu)
            {
                if (item.Content != null && item.Content.ToString() == name)
                {
                    if (hide)
                        item.Visibility = Visibility.Collapsed;
                    else
                        item.Visibility = Visibility.Visible;

                }

            }
        }

        public static void ChangeTag(string name, string tag)
        {
            foreach (var item in menu)
            {
                if (item.Content != null && item.Content.ToString() == name)
                    item.Tag = tag;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
