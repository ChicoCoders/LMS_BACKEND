namespace LMS.Models
{
    public class RequestResource
    {
        public int Id { get; set; }
        public string ResourceId { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public Resource Resource { get; set; }
    }
}
