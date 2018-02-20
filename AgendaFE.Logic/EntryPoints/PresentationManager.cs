using AgendaFE.Logic.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AgendaFE.Logic.EntryPoints
{
    public class PresentationManager
    {
        public PresentationManager()
        {

        }

        public List<string> GetUserNames()
        {
            AgendaRestClient client = new AgendaRestClient();
            return client.GetUsers().Select(u => u.Name).ToList();
        }
    }
}
