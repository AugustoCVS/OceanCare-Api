using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
