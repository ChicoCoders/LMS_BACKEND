namespace LMS.DTOs
{
    public class AddBookRequestDto
    {
        public string Type { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author {  get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
        public int Pages { get; set; }
        public int Quantity { get; set; }
        public int Location { get; set; }
        public string AddededOn {  get; set; }
        public string ImagePath {  get; set; }
        public string URL { get; set; }

    }

    public class AddBookResponseDto
    {

    }
}
