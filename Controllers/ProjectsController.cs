using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly PortfolioDbContext _db;

        public ProjectsController(PortfolioDbContext db)
        {
            _db = db;
        }

        // PUBLIC
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Projects.ToListAsync());
        }

        // ADMIN ONLY
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            return Ok(project);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Project project)
        {
            var existing = await _db.Projects.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Title = project.Title;
            existing.Description = project.Description;
            existing.TechStack = project.TechStack;
            existing.GithubUrl = project.GithubUrl;
            existing.LiveUrl = project.LiveUrl;

            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
