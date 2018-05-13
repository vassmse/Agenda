using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        private bool hasDeadlineDate;
        public bool HasDeadlineDate
        {
            get { return hasDeadlineDate; }
            set
            {
                hasDeadlineDate = value;
                NotifyPropertyChanged(nameof(HasDeadlineDate));
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

        private bool hasScheduledDate;
        public bool HasScheduledDate
        {
            get { return hasScheduledDate; }
            set
            {
                hasScheduledDate = value;
                NotifyPropertyChanged(nameof(HasScheduledDate));
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


        private int parentTaskId;
        public int ParentTaskId
        {
            get { return parentTaskId; }
            set
            {
                parentTaskId = value;
                NotifyPropertyChanged(nameof(ParentTaskId));
            }
        }

        private bool isSubTask;
        public bool IsSubTask
        {
            get { return isSubTask; }
            set
            {
                isSubTask = value;
                NotifyPropertyChanged(nameof(IsSubTask));
            }
        }


        private ObservableCollection<TaskDto> subTasks;

        public ObservableCollection<TaskDto> SubTasks
        {
            get { return subTasks; }
            set
            {
                subTasks = value;
                NotifyPropertyChanged(nameof(SubTasks));
            }
        }

        public TaskDto()
        {
            SubTasks = new ObservableCollection<TaskDto>();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
