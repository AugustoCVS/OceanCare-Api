using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanCareChat.Models
{
    public class Reports
    {
        [Key]
        public int Id { get; set; }
        public string TrashType { get; set; }
        public string TrashLocation { get; set; }
        public string TrashDescription { get; set; }
        public int OceanUserId { get; set; }
        [ForeignKey("OceanUserId")]
        public OceanUser OceanUser { get; set; } 
    }
}