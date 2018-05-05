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
                AddTask(new TaskDto { Name = "Apple", Description = "1 kg", State = 0, Priority = 0, ParentId = 3, DeadlineDate = DateTime.Now, ScheduledDate=DateTime.Now });
                AddTask(new TaskDto { Name = "Banana", Description = "", State = 0, Priority = 0, ParentId = 3, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now });
                AddTask(new TaskDto { Name = "Bread", Description = "", State = 0, Priority = 0, ParentId = 3, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now });
                AddTask(new TaskDto { Name = "Milk", Description = "", State = 0, Priority = 0, ParentId = 3, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now });
                AddTask(new TaskDto { Name = "Rice", Description = "", State = 0, Priority = 0, ParentId = 3, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now });
                AddTask(new TaskDto { Name = "Clean desk", Description = "", State = 0, Priority = 0, ParentId = 2 });
                AddTask(new TaskDto { Name = "Math Homework", Description = "", State = 0, Priority = 0, HasDeadlineDate = true, DeadlineDate = DateTime.Now.AddDays(2), HasScheduledDate = true, ScheduledDate = DateTime.Now, ParentId = 1 });
                AddTask(new TaskDto { Name = "History Essay", Description = "About Roman cities", State = 0, Priority = 0, HasDeadlineDate = true, DeadlineDate = DateTime.Now.AddDays(3), HasScheduledDate = true, ScheduledDate = DateTime.Now, ParentId = 1 });
                AddTask(new TaskDto { Name = "Buy pens", Description = "", State = 0, Priority = 0, HasDeadlineDate = true, DeadlineDate = DateTime.Now.AddDays(2), HasScheduledDate = true, ScheduledDate = DateTime.Now, ParentId = 1 });
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

        public void DeleteTask(long id, TaskDto task)
        {
            var result = Context.Tasks.SingleOrDefault(t => t.TaskId == id);
            Context.Remove(result);
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
            return new TaskDto { Id = task.TaskId, Name = task.Name, Description = task.Description, State = task.State, Priority = task.Priority, DeadlineDate = task.DeadlineDate, ScheduledDate = task.ScheduledDate, ParentId = task.CategoryId, HasDeadlineDate = task.HasDeadlineDate, HasScheduledDate = task.HasScheduledDate };
        }

        private Models.Task GetTask(TaskDto task)
        {
            return new Models.Task { Name = task.Name, Description = task.Description, State = task.State, Priority = task.Priority, DeadlineDate = task.DeadlineDate, ScheduledDate = task.ScheduledDate, Archived = false, CreatedDate = DateTime.Now, CategoryId = task.ParentId, HasDeadlineDate = task.HasDeadlineDate, HasScheduledDate = task.HasScheduledDate };
        }


    }
}
