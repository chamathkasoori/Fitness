using Fitness.Application.IServices;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Application.Services
{
    public class ZendeskService : IZendeskService
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient _client;

        private const string ContentTypeHeader = "Content-Type";
        private const string JsonContentType = "application/json";
        private const string AuthorizationHeader = "Authorization";

        public ZendeskService(IConfiguration configuration)
        {
            _configuration = configuration;

            var options = new RestClientOptions(_configuration["Zendesk:BaseUrl"] ?? string.Empty)
            {
                ThrowOnAnyError = true
            };

            _client = new RestClient(options);
        }

        public async Task<RestResponse> SendRequestAsync(string resourcePath, Method method, object requestBody)
        {
            var request = CreateRequest(resourcePath, method);
            if (requestBody != null)
            {
                request.AddJsonBody(requestBody);
            }           

            var response = await _client.ExecuteAsync(request);
            return response;
        }

        private RestRequest CreateRequest(string resourcePath, Method method = Method.Get)
        {
            var resourceUrl = _configuration["Zendesk:ResourceUrl"];
            var credentials = $"{_configuration["Zendesk:UserName"]}:{_configuration["Zendesk:Password"]}";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));

            var request = new RestRequest($"{resourceUrl}/{resourcePath}", method)
                .AddHeader(ContentTypeHeader, JsonContentType)
                .AddHeader(AuthorizationHeader, $"Basic {base64Credentials}");

            return request;
        }
    }

}
