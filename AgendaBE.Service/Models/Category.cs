using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBE.Service.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StateType { get; set; }

        [Required]
        public bool Done { get; set; }

        public string Description { get; set; }

        public List<Task> Tasks { get; set; }
        
        public int ParentUserId { get; set; }
        
        [ForeignKey("UserForeignKey")]
        public User ParentUser { get; set; }
    }
}
