using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaContracts.Models
{
    public class TaskDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public int State { get; set; }

        public int Priority { get; set; }    

        public DateTime DeadlineDate { get; set; }

        public DateTime ScheduledDate { get; set; }
    }
}
