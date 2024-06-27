using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WepApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;

        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
