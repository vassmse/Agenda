using AgendaContracts.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgendaFE.UI.Models
{
    public class AgendaRestClient
    {
        RestClient client;

        public AgendaRestClient()
        {
            client = new RestClient("http://localhost:51104/api/");
        }

        #region Category

        public List<CategoryDto> GetCategories()
        {
            var request = new RestRequest("category", Method.GET);
            var response = client.Execute<List<CategoryDto>>(request);
            return JsonConvert.DeserializeObject<List<CategoryDto>>(response.Content);
        }

        public async void AddCategory(CategoryDto category)
        {
            var request = new RestRequest("category", Method.POST);
            request.AddJsonBody(category);
            await client.ExecuteTaskAsync<List<CategoryDto>>(request);
        }

        public async void DeleteCategory(CategoryDto category)
        {
            var request = new RestRequest("category/{" + category.Id + "}", Method.DELETE);
            request.AddJsonBody(category);
            await client.ExecuteTaskAsync(request);
        }

        public async void UpdateCategory(CategoryDto category)
        {
            var request = new RestRequest("category/{" + category.Id + "}", Method.PUT);
            //request.AddParameter("id", idItem);
            request.AddJsonBody(category);
            await client.ExecuteTaskAsync(request);
        }

        #endregion


        #region Task

        public List<TaskDto> GetAllTasks()
        {
            var request = new RestRequest("task", Method.GET);
            var response = client.Execute<List<CategoryDto>>(request);
            return JsonConvert.DeserializeObject<List<TaskDto>>(response.Content);
        }


        public async void AddTask(TaskDto task)
        {
            var request = new RestRequest("task", Method.POST);
            request.AddJsonBody(task);
            await client.ExecuteTaskAsync<List<CategoryDto>>(request);
        }

        public async void UpdateTask(TaskDto task)
        {
            var request = new RestRequest("task/" + task.Id, Method.PUT);
            request.AddJsonBody(task);
            await client.ExecuteTaskAsync<List<CategoryDto>>(request);
        }

        public async void DeleteTask(TaskDto task)
        {
            var request = new RestRequest("task/" + task.Id, Method.DELETE);
            request.AddJsonBody(task);
            var response = await client.ExecuteTaskAsync<List<CategoryDto>>(request);
            Console.WriteLine(response.Content);
        }


        #endregion

    }
}
