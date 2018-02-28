using AgendaFE.Logic.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AgendaContracts.Models;

namespace AgendaFE.Logic.EntryPoints
{
    public class PresentationManager
    {
        public AgendaRestClient Client { get; set; }

        public PresentationManager()
        {
            Client = new AgendaRestClient();
        }

        public List<string> GetUserNames()
        {
            return Client.GetUsers().Select(u => u.Name).ToList();
        }

        public List<CategoryDto> GetCategories()
        {
            return Client.GetCategories();

        }

        public void AddCategory(CategoryDto newCategory)
        {
            Client.AddCategory(newCategory);
        }
    }
}
