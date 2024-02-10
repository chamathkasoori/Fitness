using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Application.IServices
{
    public interface IZendeskService
    {
        Task<RestResponse> SendRequestAsync(string resourcePath, Method method, object requestBody);
    }
}
