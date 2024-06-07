using OceanCareChat.Models;

namespace OceanCareChat.Dtos.Reports
{
    public class ReportsDTO
    {
        public class ReportsDto
        {
            public int Id { get; set; }
            public string TrashType { get; set; }
            public string TrashLocation { get; set; }
            public string TrashDescription { get; set; }
            public int OceanUserId { get; set; }
            public string OceanUserName { get; set; }
        }
    }
}
