using AgendaBE.Service.Models;
using AgendaContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBE.Service.Helpers
{
    public class DBHelper
    {
        private DataBaseContext _context; //TODO: Repository Pattern, controllernek saját service

        public DBHelper(DataBaseContext context)
        {
            _context = context;
            if (_context.Categories.Count() == 0)
            {
                AddCategory(new CategoryDto { Name = "School", Description = "blaa", Done = false, StateType = StateTypes.Checklist });
                AddCategory(new CategoryDto { Name = "Work", Description = "blaa bla bla", Done = false, StateType = StateTypes.Checklist });
                AddCategory(new CategoryDto { Name = "Shopping", Description = "blaa", Done = false, StateType = StateTypes.Kanban3 });
            }

        }

        public List<CategoryDto> GetCategories()
        {
            return GetCategoryDto(_context.Categories.ToList());
        }

        public CategoryDto GetCategory(int id)
        {
            return GetCategoryDto(_context.Categories.FirstOrDefault(t => t.CategoryId == id));
        }

        public void AddCategory(CategoryDto category)
        {
            _context.Categories.Add(GetCategory(category));
            _context.SaveChanges();
        }

        private List<CategoryDto> GetCategoryDto(List<Category> categories)
        {
            var CategoryDtoList = new List<CategoryDto>();
            foreach (var category in categories)
            {
                CategoryDtoList.Add(GetCategoryDto(category));
            }
            return CategoryDtoList;
        }

        private CategoryDto GetCategoryDto(Category category)
        {
            return new CategoryDto { Name = category.Name, Description = category.Description, Done = category.Done, StateType = (StateTypes)Enum.Parse(typeof(StateTypes), category.StateType) };
        }

        private Category GetCategory(CategoryDto category)
        {
            return new Category { Name = category.Name, Description = category.Description, Done = category.Done, StateType = category.StateType.ToString() };
        }

    }
}
