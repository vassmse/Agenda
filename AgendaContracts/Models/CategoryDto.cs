using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AgendaContracts.Models
{
    public class CategoryDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

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

        private string oldName;
        public string OldName
        {
            get { return oldName; }
            set
            {
                oldName = value;
                NotifyPropertyChanged(nameof(OldName));
            }
        }

        private StateTypes stateType;
        public StateTypes StateType
        {
            get { return stateType; }
            set { stateType = value; }
        }


        private bool done;
        public bool Done
        {
            get { return done; }
            set
            {
                done = value;
                NotifyPropertyChanged(nameof(Done));
            }
        }

        private bool visibility;
        public bool Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                NotifyPropertyChanged(nameof(Visibility));
            }
        }

        private bool renaming;
        public bool Renaming
        {
            get { return renaming; }
            set
            {
                renaming = value;
                NotifyPropertyChanged(nameof(Renaming));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }    


        public ObservableCollection<TaskDto> Tasks { get; set; }


        public CategoryDto()
        {
            Tasks = new ObservableCollection<TaskDto>();
            Visibility = true;
        }

        public StateTypes StateType1 { get => stateType; set => stateType = value; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum StateTypes
    {
        Checklist,
        MultiChecklist,
        Kanban3,
        Kanban5
    }
}
