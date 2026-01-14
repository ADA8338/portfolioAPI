using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string TechStack { get; set; } = string.Empty;

        public string ProjectUrl { get; set; } = string.Empty;
    }
}
