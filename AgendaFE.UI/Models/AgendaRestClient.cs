﻿using AgendaContracts.Models;
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

        public List<CategoryDto> GetCategories()
        {
            var request = new RestRequest("category", Method.GET);
            var response = client.Execute<List<CategoryDto>>(request);
            return JsonConvert.DeserializeObject<List<CategoryDto>>(response.Content);
        }

        public void AddCategory(CategoryDto category)
        {
            var request = new RestRequest("category", Method.POST);
            request.AddJsonBody(category);
            client.Execute<List<CategoryDto>>(request);
        }

        public void DeleteCategory(int id)
        {
            var request = new RestRequest("api/item/{"+id+"}", Method.DELETE);
            //request.AddParameter("id", idItem);
            client.Execute(request);
        }
    }
}