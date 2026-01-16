using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models
{
    public enum Position
    {
        None,
        Programmer,
        Analyst,
        Designer
    }

    public class Employee
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        public Position CurrentPosition { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}