using AgendaContracts.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaFE.Logic.Communication
{
    class AgendaRestClient
    {
        const string BaseUrl = "http://localhost:51104/api/agenda";

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

    }
}