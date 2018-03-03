﻿using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Options;
using searchresults_api.Configuration;
using searchresults_api.Contracts;
using static Google.Apis.Customsearch.v1.CseResource;

namespace searchresults_api.Factory
{
    public class GoogleApiFactory : IGoogleApiFactory
    {
        private readonly string apiKey;
        private readonly string engineId;
        private readonly CustomsearchService client;

        public GoogleApiFactory(IOptions<GoogleApiSettings> settings)
        {
            apiKey = settings.Value.ApiKey;
            engineId = settings.Value.EngineId;
            client = new CustomsearchService(new BaseClientService.Initializer { ApiKey = settings.Value.ApiKey });

        }

        public ListRequest CreateRequest(string query)
        {
            var listRequest = client.Cse.List(query);
            listRequest.Cx = engineId;

            return listRequest;
        }
    }
}