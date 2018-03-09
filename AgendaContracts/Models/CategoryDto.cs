using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AgendaContracts.Models
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public StateTypes StateType { get; set; }

        public bool Done { get; set; }

        public string Description { get; set; }

        public ObservableCollection<TaskDto> Tasks { get; set; }
    }

    public enum StateTypes
    {
        Checklist,
        MultiChecklist,
        Kanban3,
        Kanban5
    }
}
