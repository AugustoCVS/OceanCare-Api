using OceanCareChat.Dtos.User;

namespace OceanCareChat.Dtos.Events
{
    public class EventsDTO
    {
        public class EventsDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
        }
    }
}
