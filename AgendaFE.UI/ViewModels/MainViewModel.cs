using AgendaFE.Logic.EntryPoints;
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
    public class MainViewModel : INotifyPropertyChanged
    {
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
            foreach (var user in businessLayer.GetUserNames())
            {
                UserNames.Add(user);
            }

        }

        //public ICommand AddUserCommand
        //{
        //    get
        //    {
        //        return new CommandHandler(() => this.AddUserAction());
        //    }
        //}

        //private void AddUserAction()
        //{
        //    UserNames.Add(NewUserName);
        //    NewUserName = String.Empty;

        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
