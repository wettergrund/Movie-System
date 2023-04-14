global using MovieSystem.Models;
using Newtonsoft.Json;

namespace MovieSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //await ListUsers();
            await ListMovies();
        }

        static async Task ListUsers()
        {
            string userUrl = "https://localhost:7107/users";

            HttpClient client = new HttpClient();

            dynamic userResponse = await client.GetStringAsync(userUrl);

            List<User> userList = JsonConvert.DeserializeObject<List<User>>(userResponse);

            foreach (User item in userList)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Email);
            }

        }

        static async Task ListMovies()
        {
            Console.WriteLine("Ange en film");
            string? answer = Console.ReadLine();

            string userUrl = $"https://localhost:7107/movie/{answer}";

            HttpClient client = new HttpClient();

            dynamic userResponse = await client.GetStringAsync(userUrl);

            Result result = JsonConvert.DeserializeObject<Result>(userResponse);


            foreach (Movie movie in result.Results)
            {
                Console.WriteLine(movie.Title);
                Console.WriteLine(movie.AverageScore);
            }

        }
    }
}