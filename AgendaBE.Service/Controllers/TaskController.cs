using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaBE.Service.Models;
using AgendaBE.Service.Services;
using AgendaContracts.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaBE.Service.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private TaskService Service { get; }

        public TaskController(DataBaseContext context)
        {
            Service = new TaskService(context);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TaskDto> Get()
        {
            return Service.GetAllTasks();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Create([FromBody] TaskDto item)
        {
            Service.AddTask(item);           
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
