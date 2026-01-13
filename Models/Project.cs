using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Project
    {
        // Primary Key
        public int Id { get; set; }

        // Project title (required)
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        // Short description
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        // Technologies used (YOLOv8, .NET, React, etc.)
        [MaxLength(300)]
        public string TechStack { get; set; } = string.Empty;

        // GitHub repository link
        [Url]
        public string GitHubUrl { get; set; } = string.Empty;

        // Live demo URL (optional)
        [Url]
        public string LiveUrl { get; set; } = string.Empty;

        // When project was added
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
