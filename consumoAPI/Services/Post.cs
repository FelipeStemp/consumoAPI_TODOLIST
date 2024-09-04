using ConsumoAPI.Models;
using Newtonsoft.Json;
using System.Text;


namespace ConsumoAPI.Services
{
    public class Post
    {

        private readonly HttpClient _httpClient;

        public Post(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Tarefa> CreateItem(Tarefa item)
        {

            var url = "https://api-to-do-list-brown.vercel.app/createItem";

            var JsonContent = JsonConvert.SerializeObject(item);

            var content = new StringContent(JsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var createdItem = JsonConvert.DeserializeObject<Tarefa>(responseString)!;

                return createdItem;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar usuário: {response.StatusCode} - {errorMessage}");
            }
        }
    }
}
