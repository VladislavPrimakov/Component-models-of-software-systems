namespace Task5.Models
{
    public class RiskViewModel
    {
        public Risk Risk { get; set; } = new Risk();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
