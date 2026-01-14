using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.DTOs
{
    public class ProjectCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string TechStack { get; set; } = string.Empty;

        [Url]
        public string GithubUrl { get; set; } = string.Empty;

        [Url]
        public string? LiveUrl { get; set; }
    }
}
