using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services;

public class WebhookEmitterService
{
    private readonly HttpClient _httpClient;

    public WebhookEmitterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task EmitWebhookAsync(string url, WebhookEvent webhookEvent)
    {
        var jsonContent = JsonSerializer.Serialize(webhookEvent);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            // Handle failure (e.g., log the error, retry, etc.)
        }
    }
}