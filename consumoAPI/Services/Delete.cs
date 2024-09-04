using ConsumoAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace ConsumoAPI.Services
{
    public class Delete
    {
        private readonly HttpClient _httpClient;

        public Delete(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task DeleteItem(Tarefa tarefa)
        {
            var url = "https://api-to-do-list-brown.vercel.app/delete";

            var reqBody = new { id = tarefa._id, name = tarefa.name };
            var jsonContent = JsonConvert.SerializeObject(reqBody);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
        }
    }
}
