using ConsumoAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace ConsumoAPI.Services
{
    public class Put
    {
        private readonly HttpClient _httpClient;

        public Put(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
            
        public async Task<Tarefa> PutItem(Tarefa item)
        {
            var url = $"https://api-to-do-list-brown.vercel.app/updateByID/{item._id}";

            var JsonContent = JsonConvert.SerializeObject(item);

            var content = new StringContent(JsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var updateItem = JsonConvert.DeserializeObject<Tarefa>(responseString);

                return updateItem;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao atualizar usuário: {response.StatusCode} - {errorMessage}");
            }

        }
    }
}
