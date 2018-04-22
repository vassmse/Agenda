using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBE.Service.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int State { get; set; }

        public int Priority { get; set; }

        [Required]
        public bool Archived { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public bool HasDeadlineDate { get; set; }

        public DateTime DeadlineDate { get; set; }

        public bool HasScheduledDate { get; set; }

        public DateTime ScheduledDate { get; set; }
        
        public int ParentTaskId { get; set; }

        [ForeignKey("TaskForeignKey")]
        public Task ParentTask { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey("CategoryForeignKey")]
        public Category ParentCategory { get; set; }

    }
}
