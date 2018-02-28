using AgendaContracts.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaFE.Logic.Communication
{
    public class AgendaRestClient
    {
        const string BaseUrl = "http://localhost:51104/api/";

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }

        public List<UserDto> GetUsers()
        {
            var request = new RestRequest();

            return Execute<List<UserDto>>(request);
        }

        public List<CategoryDto> GetCategories()
        {
            var request = new RestRequest("category");

            return Execute<List<CategoryDto>>(request);
        }

        public void AddCategory(CategoryDto newCategory)
        {
            var request = new RestRequest("category", Method.POST);
            request.AddJsonBody(newCategory);
            Execute<List<CategoryDto>>(request);
        }

    }
}