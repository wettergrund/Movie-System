using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public int ExtID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Link { get; set; }

        public int AddedBy { get; set; }



    }
}
