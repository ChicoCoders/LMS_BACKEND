using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class FirebaseConnection
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public string userName { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
