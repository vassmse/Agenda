using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaContracts.Models
{
    public class UserDto
    {        
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }
    }
}
