using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace api.dane.gov.pl
{
    public class ClientApi
    {
        private HttpClient _client;

        // tworzy
        public ClientApi()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept" ,"application/vnd.api+json");
            this._client = client;
        }

        public Task<List<string>> GetWomenLastnames(uint amount)
        {
            return _GetData<LastnameResponse>(amount, Endpoints.WomanLastname);
        }

        public Task<List<string>> GetMenLastnames(uint amount)
        {
            return _GetData<LastnameResponse>(amount, Endpoints.ManLastname);
        }

        public Task<List<string>> GetWomenNames(uint amount)
        {
            return _GetData<FirstnameResponse>(amount, Endpoints.WomanNames);
        }

        public Task<List<string>> GetMenNames(uint amount)
        {
            return _GetData<FirstnameResponse>(amount, Endpoints.ManNames);
        }

        private async Task<List<string>> _GetData<T>(uint amount, string url) where T : ResponseType
        {
            var data = new List<string>();

            var pages = (int)Math.Ceiling(amount / 100.0);

            for(uint page = 1; page <= pages; page++)
            {
                var per_page = amount > 100 ? 100 : amount;
                var last_data = await _MakeRequest<T>(url, page, per_page);

                amount = (long)amount - last_data.Count < 0 ? 0 : (uint)(amount - last_data.Count);

                data = data.Concat(last_data).ToList();
            }

            return data;
        }

        private async Task<List<string>> _MakeRequest<T>(string url,uint page, uint per_page) where T : ResponseType
        {
            url += $"?page={page}&per_page={per_page}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using (var contentStream = await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<T>(contentStream);
                return data.GetData();
            }

        }

    }
}
