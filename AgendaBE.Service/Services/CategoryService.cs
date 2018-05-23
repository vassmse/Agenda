using AgendaBE.Service.Models;
using AgendaContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBE.Service.Services
{
  public class CategoryService
  {
    private DataBaseContext Context { get; set; }

    public CategoryService(DataBaseContext dbContext)
    {
      Context = dbContext;

      if (Context.Categories.Count() == 0)
      {
        AddCategory(new CategoryDto { Name = "Iskola", Description = "", StateType = StateTypes.Kanban3 });
        AddCategory(new CategoryDto { Name = "Munka", Description = "", StateType = StateTypes.MultiChecklist });
        AddCategory(new CategoryDto { Name = "Bevásárlás", Description = "", StateType = StateTypes.Checklist });
        AddCategory(new CategoryDto { Name = "Agenda szoftver", Description = "", StateType = StateTypes.Kanban5 });

            }
    }

    public List<CategoryDto> GetCategories()
    {
      return GetCategoryDto(Context.Categories.ToList());
    }

    public CategoryDto GetCategory(int id)
    {
      return GetCategoryDto(Context.Categories.FirstOrDefault(t => t.CategoryId == id));
    }

    public List<Category> GetCategories2()
    {
      return Context.Categories.ToList();
    }

    public void AddCategory(CategoryDto category)
    {
      Context.Categories.Add(GetCategory(category));
      Context.SaveChanges();
    }

    public void DeleteCategory(CategoryDto category)
    {
      var result = Context.Categories.SingleOrDefault(c => c.CategoryId == category.Id);
      Context.Remove(result);
      Context.SaveChanges();
    }

    public void UpdateCategory(CategoryDto category)
    {
      var result = Context.Categories.SingleOrDefault(c => c.CategoryId == category.Id);
      result.Name = category.Name;
      result.StateType = category.StateType.ToString();
      Context.SaveChanges();
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
      return new CategoryDto { Id = category.CategoryId, Name = category.Name, Description = category.Description, Done = category.Done, StateType = (StateTypes)Enum.Parse(typeof(StateTypes), category.StateType) };
    }

    private Category GetCategory(CategoryDto category)
    {
      return new Category { Name = category.Name, Description = category.Description, Done = category.Done, StateType = category.StateType.ToString() };
    }
  }
}
