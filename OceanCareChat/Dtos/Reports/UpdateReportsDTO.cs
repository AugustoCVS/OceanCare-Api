using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.Reports
{
    public class UpdateReportsDTO
    {

        public string TrashType { get; set; }

        public string TrashLocation { get; set; }

        public string TrashDescription { get; set; }
    }
}
