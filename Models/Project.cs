using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string TechStack { get; set; }

        public string GithubUrl { get; set; }

        public string LiveUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
