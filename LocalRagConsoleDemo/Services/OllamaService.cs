using System.Net.Http.Json;
using System.Text.Json;
using LocalRagConsoleDemo.Models;

namespace LocalRagConsoleDemo.Services;

public interface IOllamaService
{
    IAsyncEnumerable<string> GetCompletionStreamAsync(string prompt);
}

public class OllamaService : IOllamaService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:11434/api/generate";

    public OllamaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async IAsyncEnumerable<string> GetCompletionStreamAsync(string prompt)
    {
        var request = new OllamaRequest { Model = "phi4", Prompt = prompt };
        var reqMsg = new HttpRequestMessage(HttpMethod.Post, BaseUrl)
        {
            Content = JsonContent.Create(request)
        };

        using var response = await _httpClient.SendAsync(reqMsg, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
       
        await using var stream = await response.Content.ReadAsStreamAsync();
        using var sr = new StreamReader(stream);
       
        await using var jsonReader = new Newtonsoft.Json.JsonTextReader(sr)
        {
            SupportMultipleContent = true
        };
        var serializer = new Newtonsoft.Json.JsonSerializer();

        while (await jsonReader.ReadAsync())
        {
            if (jsonReader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
            {
                var obj = serializer.Deserialize<OllamaResponse>(jsonReader);
                if (obj != null && !string.IsNullOrEmpty(obj.Response) && !obj.Done)
                    yield return obj.Response;
            }
        }
    }


}