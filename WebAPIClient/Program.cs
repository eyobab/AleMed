using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Collections.Generic;


namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = client.GetStringAsync("http://localhost:5000/api/commands");
            
            // var stringTask = client.GetStringAsync("http://localhost:5000/api/commands/1");
            
            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            // var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            var msg = await stringTask;
            Console.Write(msg.ToString());
        }
    }
}
