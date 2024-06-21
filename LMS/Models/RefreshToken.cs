using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
