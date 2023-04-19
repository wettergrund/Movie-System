using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Genre : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ExtID { get; set; }
    }
}
