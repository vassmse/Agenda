﻿using System;
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
    public class CategoryController : Controller
    {
        private CategoryService Service { get; }

        public CategoryController(DataBaseContext context)
        {
            Service = new CategoryService(context);
            
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<CategoryDto> GetAll()
        {
            return Service.GetCategories();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id)
        {
            var category = Service.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return new ObjectResult(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Service.AddCategory(item);

            return CreatedAtRoute("GetCategory", new { id = 0 }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] CategoryDto category)
        {
            Service.DeleteCategory(category);
            return new NoContentResult();
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] CategoryDto category)
        {
            Service.UpdateCategory(category);
            return new NoContentResult();
        }




    }
}
