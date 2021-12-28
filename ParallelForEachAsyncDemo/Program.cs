using System.Net.Http.Headers;
using System.Net.Http.Json;

var userHandlers = new[]
{
    "users/mukkalajagan",
    "users/okyrylchuk",
    "users/shanselman",
    "users/jaredpar",
    "users/davidfowl"
};

using HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://api.github.com");
client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DotNet", "6"));

ParallelOptions parallelOptions = new()
{
    MaxDegreeOfParallelism = 3
};

await Parallel.ForEachAsync(userHandlers, parallelOptions, async (uri, token) =>
{
    var user = await client.GetFromJsonAsync<GitHubUser>(uri, token);

    Console.WriteLine($"Name: {user.Name}\nBio: {user.Bio}\n");
});
Console.ReadLine();

public record GitHubUser(string Name, string Bio);