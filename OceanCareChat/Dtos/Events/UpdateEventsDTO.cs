using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.Events
{
    public class UpdateEventsDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
