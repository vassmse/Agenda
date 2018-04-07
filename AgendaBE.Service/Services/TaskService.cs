using AgendaBE.Service.Models;
using AgendaContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBE.Service.Services
{
    public class TaskService
    {
        private DataBaseContext Context { get; set; }

        public TaskService(DataBaseContext dbContext)
        {
            Context = dbContext;

            if (Context.Tasks.Count() == 0)
            {
                AddTask(new TaskDto { Name = "Apple", Description = "blaa", State = 0, Priority = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId=1 });
                AddTask(new TaskDto { Name = "Banana", Description = "blaa bla bla", State = 0, Priority = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId=1 });
                AddTask(new TaskDto { Name = "Bread", Description = "blaa", State = 0, Priority = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, ParentId=2 });
            }
        }

        public List<TaskDto> GetAllTasks()
        {
            return GetTaskDto(Context.Tasks.ToList());
        }

        public void AddTask(TaskDto task)
        {
            Context.Tasks.Add(GetTask(task));
            Context.SaveChanges();
        }

        public void UpdateTask(long id, TaskDto task)
        {
            var result = Context.Tasks.SingleOrDefault(t => t.TaskId == id);
            Context.Entry(result).CurrentValues.SetValues(task);
            Context.SaveChanges();
        }



        private List<TaskDto> GetTaskDto(List<Models.Task> tasks)
        {
            var CategoryDtoList = new List<TaskDto>();
            foreach (var category in tasks)
            {
                CategoryDtoList.Add(GetTaskDto(category));
            }
            return CategoryDtoList;
        }

        private TaskDto GetTaskDto(Models.Task task)
        {
            return new TaskDto { Id = task.TaskId, Name = task.Name, Description = task.Description, State = task.State, Priority = task.Priority, DeadlineDate = task.DeadlineDate, ScheduledDate = task.ScheduledDate, ParentId=task.CategoryId };
        }

        private Models.Task GetTask(TaskDto task)
        {
            return new Models.Task { Name = task.Name, Description = task.Description, State = task.State, Priority = task.Priority, DeadlineDate = task.DeadlineDate, ScheduledDate = task.ScheduledDate, Archived = false, CreatedDate = DateTime.Now, CategoryId = task.ParentId };
        }


    }
}
