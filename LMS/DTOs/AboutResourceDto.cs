namespace LMS.DTOs
{
    public class AboutResourceDto
    {
        public string ISBN { get; set; }
        public string Author {  get; set; }
        public int Remain {  get; set; }
        public int borrowed { get; set; }
        public int total {  get; set; }
        public int CupboardId {  get; set; }
        public int ShelfId { get; set; }
        public string Description { get; set; }
        public int pages {  get; set; }
        public double price { get; set; }
        public DateTime addedon {  get; set; }


    }
}
