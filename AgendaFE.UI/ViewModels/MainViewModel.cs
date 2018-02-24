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
        public RelayCommand AddUserCommand { get; private set; }

        private ObservableCollection<string> userNames;

        public ObservableCollection<string> UserNames
        {
            get { return userNames; }
            set
            {
                userNames = value;
                RaisePropertyChanged(nameof(UserNames));
            }
        }

        private string newUserName;

        public string NewUserName
        {
            get { return newUserName; }
            set
            {
                newUserName = value;
                RaisePropertyChanged(nameof(NewUserName));
            }
        }

        private PresentationManager businessLayer { get; set; }

        public MainViewModel()
        {
            businessLayer = new PresentationManager();
            UserNames = new ObservableCollection<string>();
            //AddUserCommand = new RelayCommand(AddUser);
            //foreach (var user in businessLayer.GetUserNames())
            //{
            //    UserNames.Add(user);
            //}
            //AddUserCommand.RaiseCanExecuteChanged();

        }

        public bool CanAddUser()
        {
            return true;
        }

        public void AddUser()
        {
            UserNames.Add(NewUserName);
            NewUserName = String.Empty;
        }
    }
}
