using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AspnetRunBasics.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CatalogService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        // public CatalogService(HttpClient client) =>
        //     _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");


          //var response = await _client.GetAsync("/Catalog",HttpCompletionOption.ResponseHeadersRead);
           // return await response.ReadContentAs<List<CatalogModel>>();

          //  var httpClient = _client.CreateClient("MovieAPIClient");

          var request = new HttpRequestMessage(
              HttpMethod.Get,
              "/Catalog");

          var response = await httpClient.SendAsync(
              request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

          response.EnsureSuccessStatusCode();

          var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CatalogModel>>(content);

        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            throw new NotImplementedException();
            // var response = await _client.GetAsync($"/Catalog/{id}");
            // return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            throw new NotImplementedException();
            // var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{category}");
            // return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            throw new NotImplementedException();
            // var response = await _client.PostAsJson($"/Catalog", model);
            // if (response.IsSuccessStatusCode)
            // {
            //     return await response.ReadContentAs<CatalogModel>();
            // }
            // else
            // {
            //     throw new Exception("Something went wrong when calling api.");
            // }
        }
    }
}
