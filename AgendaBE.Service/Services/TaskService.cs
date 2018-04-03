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
                AddTask(new TaskDto { Name = "Apple", Description = "blaa", State = 0, Priority = 0, DeadlineDate=DateTime.Now, ScheduledDate=DateTime.Now });
                AddTask(new TaskDto { Name = "Banana", Description = "blaa bla bla", State = 0, Priority = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now });
                AddTask(new TaskDto { Name = "Bread", Description = "blaa", State = 0, Priority = 0, DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now });
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
            return new TaskDto { Name = task.Name, Description = task.Description, State = task.State, Priority=task.Priority, DeadlineDate=task.DeadlineDate, ScheduledDate = task.ScheduledDate };
        }

        private Models.Task GetTask(TaskDto task)
        {
            return new Models.Task { Name = task.Name, Description = task.Description, State = task.State, Priority = task.Priority, DeadlineDate = task.DeadlineDate, ScheduledDate = task.ScheduledDate, Archived=false, CreatedDate=DateTime.Now };
        }



    }
}
