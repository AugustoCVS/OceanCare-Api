using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Models
{
    public class OceanUser
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReportedTrash { get; set; }

        public ICollection<Reports> Reports { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}