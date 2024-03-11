using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Location
    {
        [Key]
        public int LocationNo { get; set; }
        public string name {  get; set; }
        public List<Resource> resources { get; set; }
    }
}
