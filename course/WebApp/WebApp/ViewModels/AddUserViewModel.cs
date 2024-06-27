using WepApp.Models;

namespace WebApp.ViewModels
{
    public class AddUserViewModel
    {
        public User User {  get; set; } = new User();
        public List<Role>? Roles { get; set; }
    }
}
