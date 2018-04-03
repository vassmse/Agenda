using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AgendaContracts.Models
{
    public class TaskDto : INotifyPropertyChanged
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

        private int state;
        public int State
        {
            get { return state; }
            set
            {
                state = value;
                NotifyPropertyChanged(nameof(State));
            }
        }

        private int priority;
        public int Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                NotifyPropertyChanged(nameof(Priority));
            }
        }

        private DateTime deadlineDate;
        public DateTime DeadlineDate
        {
            get { return deadlineDate; }
            set
            {
                deadlineDate = value;
                NotifyPropertyChanged(nameof(DeadlineDate));
            }
        }

        private DateTime scheduledDate;
        public DateTime ScheduledDate
        {
            get { return scheduledDate; }
            set
            {
                scheduledDate = value;
                NotifyPropertyChanged(nameof(ScheduledDate));
            }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set
            {
                parentId = value;
                NotifyPropertyChanged(nameof(ParentId));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
