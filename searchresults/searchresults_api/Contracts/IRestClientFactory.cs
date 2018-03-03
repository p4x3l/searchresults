using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchresults_api.Contracts
{
    public interface IRestClientFactory
    {
        RestClient Create(string baseUrl);

        RestRequest CreateRequest(string url, Method method);
    }
}
