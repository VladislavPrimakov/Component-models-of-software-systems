using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ViewResumeViewModel
    {
        public Resume Resume { get; set; } = new Resume();

        [Required]
        [Display(Name = "Select Job")]
        public int jobId { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
