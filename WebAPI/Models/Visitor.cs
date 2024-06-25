using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Visitor
    {
        [Key]
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string VisitDate { get; set; }
        public string VisitorPurpose { get; set; }
    }
}
