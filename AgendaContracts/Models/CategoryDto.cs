using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaContracts.Models
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public string StateType { get; set; }

        public bool Done { get; set; }

        public string Description { get; set; }
    }
}
