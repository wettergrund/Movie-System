using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models
{
    [Keyless]
    public class v_userGenreInfo
    {
        public string Name { get; set; }    
        public string Title { get; set; }
        public int GID { get; set; }
        public int UID { get; set; }
    }
}
