using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string ViolationDate { get; set; }
        public string ViolationDescription { get; set; }
    }
}
