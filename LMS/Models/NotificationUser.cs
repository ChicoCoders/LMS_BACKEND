using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class NotificationUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public string UserName { get; set; }

        [ForeignKey(nameof(Notifications))]
        public int NotificationId { get; set; }

        public string Status { get; set; }
        
        public User User { get; set; }

        public Notifications Notifications { get; set; }
    }
}
