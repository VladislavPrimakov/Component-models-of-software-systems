using System.ComponentModel.DataAnnotations;
using WepApp.Models;

namespace WebApp.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }
        
        [Required]
        public string CompanyName { get; set; } = String.Empty;

        [Required]
        public string CompanyDescription { get; set; } = String.Empty;

        [Required]
        public string Contacts {  get; set; } = String.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
