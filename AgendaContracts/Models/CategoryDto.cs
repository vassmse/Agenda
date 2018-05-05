using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public Array StateTypeValues { get; set; }


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

        //TODO:!!! szépen!
        private ObservableCollection<TaskDto> toDoTasks;

        public ObservableCollection<TaskDto> ToDoTasks
        {
            get { return toDoTasks; }
            set
            {
                toDoTasks = value;
                NotifyPropertyChanged(nameof(ToDoTasks));
            }
        }

        private ObservableCollection<TaskDto> doingTasks;

        public ObservableCollection<TaskDto> DoingTasks
        {
            get { return doingTasks; }
            set
            {
                doingTasks = value;
                NotifyPropertyChanged(nameof(DoingTasks));
            }
        }

        private ObservableCollection<TaskDto> doneTasks;

        public ObservableCollection<TaskDto> DoneTasks
        {
            get { return doneTasks; }
            set
            {
                doneTasks = value;
                NotifyPropertyChanged(nameof(DoneTasks));
            }
        }


        public CategoryDto()
        {
            Tasks = new ObservableCollection<TaskDto>();
            ToDoTasks = new ObservableCollection<TaskDto>();
            DoingTasks = new ObservableCollection<TaskDto>();
            DoneTasks = new ObservableCollection<TaskDto>();
            Visibility = true;
            StateTypeValues = Enum.GetValues(typeof(StateTypes));
        }


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
