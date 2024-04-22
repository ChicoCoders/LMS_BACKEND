namespace LMS.DTOs
{
    public class NewNoticeDto
    {
        public int Id { get; set; }
        public string UserName {  get; set; }
        public string Subject {  get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
    }

    public class NoticeDto
    {
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        
    }
}
