namespace MovieSystem
{
    public class Result
    {
        public string? Page { get; set; }
        public List<Movie> Results { get; set; }
        public List<User> Users { get; set; }
        public int Id { get; set; }
    }
}
