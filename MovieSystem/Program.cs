﻿//global using MovieSystem.Models;
using MovieAPI.Models;
using Newtonsoft.Json;

namespace MovieSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //await ListUsers();
            //await GetGenreName();
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
                Console.WriteLine();
                ShowScore(movie.AverageScore);
                if(movie.Gendres.Count > 0)
                {

                    foreach (var genre in movie.Gendres)
                    {
                       
                        Console.WriteLine(await GetGenreName(genre));
                    }
                    Console.WriteLine("---");
                }
                


            }

        }

        static async Task<string?> GetGenreName(int genreNr)
        {

            //https://localhost:7107/genres

            string genreUrl = "https://localhost:7107/genres";

            HttpClient client = new HttpClient();
            dynamic response = await client.GetStringAsync(genreUrl);

            List<Genre> genreList = JsonConvert.DeserializeObject<List<Genre>>(response);

            
                string? genreTitle = (from g in genreList
                              where g.ExtID.Equals(genreNr)
                                 select g.Title).LastOrDefault();

            return genreTitle;
           
        }

        static void ShowScore(decimal score)
        {
            switch (score)
            {
                case decimal n when (n == 0):
                    Console.WriteLine("No score");
                    break;
                case decimal n when (n < 2):
                    Console.WriteLine("⭐");
                    break;
                case decimal n when (n < 4):
                    Console.WriteLine("⭐⭐");
                    break;
                case decimal n when (n < 6):
                    Console.WriteLine("⭐⭐⭐");
                    break;
                case decimal n when (n < 8):
                    Console.WriteLine("⭐⭐⭐⭐");
                    break;
                case decimal n when (n <= 10):
                    Console.WriteLine("⭐⭐⭐⭐⭐");
                    break;
            }
        }

    }
}