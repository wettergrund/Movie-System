using Newtonsoft.Json;

namespace MovieSystem
{
    public class Movie
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public List<int> Gendres  { get; set; }

        public decimal AverageScore { get; set; }

        public string Overview { get; set; }

    }
}
