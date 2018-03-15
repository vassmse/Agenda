using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AgendaContracts.Models
{
    public class UserDto: INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string passwordHash;
        public string PasswordHash
        {
            get { return passwordHash; }
            set
            {
                passwordHash = value;
                NotifyPropertyChanged(nameof(PasswordHash));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
