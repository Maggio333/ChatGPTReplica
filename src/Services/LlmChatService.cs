using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AS.ChatGPTReplica.Services
{
    public class LlmChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public LlmChatService(string endpointUrl)
        {
            _httpClient = new HttpClient();
            _endpointUrl = endpointUrl;
        }

        public async Task<string> SendPromptAsync(string prompt)
        {
            var requestBody = new
            {
                model = "mistral",
                prompt = prompt,
                stream = false
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_endpointUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(responseString);
            return document.RootElement.GetProperty("response").GetString() ?? string.Empty;
        }
    }
}