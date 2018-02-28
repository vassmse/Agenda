using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaBE.Service.Models;
using AgendaContracts.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaBE.Service.Controllers
{
    [Route("api/[controller]")]
    public class AgendaController : Controller
    {
        private readonly DataBaseContext _context;
        private List<UserDto> users = new List<UserDto>();
        private List<CategoryDto> categories = new List<CategoryDto>();

        public AgendaController(DataBaseContext context)
        {
            _context = context;            

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { Name = "User1", PasswordHash = "1234", Email = "a@b.com", RegisterDate = DateTime.Now });
                _context.Users.Add(new User { Name = "User2", PasswordHash = "almafa", Email = "alma@b.com", RegisterDate = DateTime.Now });
                users.Add(new UserDto { Name = "User1", PasswordHash = "1234", Email = "a@b.com" });
                users.Add(new UserDto { Name = "User2", PasswordHash = "almafa", Email = "alma@b.com" });
                _context.SaveChanges();
            }

            if (_context.Categories.Count() == 0)
            {
                User user = new User { Name = "User0", PasswordHash = "assd", Email = "a111@b.com", RegisterDate = DateTime.Now };
                _context.Users.Add(user);
                users.Add(new UserDto { Name = "User0", PasswordHash = "assd", Email = "a111@b.com" });
                _context.Categories.Add(new Category { Name = "School", Description = "BME", Done = false, StateType = StateTypes.Checklist.ToString(), ParentUser = user, ParentUserId = user.UserId });
                categories.Add(new CategoryDto { Name = "School", Description = "BME", Done = false, StateType = StateTypes.Checklist });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<UserDto> GetAll()
        {
            return users;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var item = _context.Users.FirstOrDefault(t => t.UserId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Users.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.UserId }, item);
        }





        //// GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

