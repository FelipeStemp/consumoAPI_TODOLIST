using ConsumoAPI.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace ConsumoAPI.Services
{
    public class Get
    {

        private readonly HttpClient _httpClient;

        public Get(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Tarefa>> getAll()
        {
            var url = "https://api-to-do-list-brown.vercel.app/";

            var response = await _httpClient.GetAsync(url);

            var JsonString = await response.Content.ReadAsStringAsync();

            List<Tarefa> result = JsonConvert.DeserializeObject<List<Tarefa>>(JsonString)!;

            return result;
        }
        public async Task<Tarefa> getName(string name)
        {
            var url = $"https://api-to-do-list-brown.vercel.app/name/{name}";
            var response = await _httpClient.GetAsync(url);

            var JsonString = await response.Content.ReadAsStringAsync();

            Tarefa itemObjt = JsonConvert.DeserializeObject<Tarefa>(JsonString)!;

            return itemObjt;
        }

        public async Task<Tarefa> getId(string id)
        {
            var url = $"https://api-to-do-list-brown.vercel.app/id/{id}";
            var response = await _httpClient.GetAsync(url);

            var JsonString = await response.Content.ReadAsStringAsync();

            Tarefa itemObjt = JsonConvert.DeserializeObject<Tarefa>(JsonString)!;

            return itemObjt;
        }
    }
}
