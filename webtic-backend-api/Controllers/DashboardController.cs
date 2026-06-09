using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using WebTIC.API.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebTIC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class DashboardController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var totalUsers = await _userManager.Users.CountAsync();
            var activeUsers = await _userManager.Users.CountAsync(u => u.IsActive);
            var inactiveUsers = totalUsers - activeUsers;

            // Obtener roles y conteo (requiere agrupar desde UserRoles)
            var rolesDistribution = new Dictionary<string, int>();
            var roles = await _roleManager.Roles.ToListAsync();
            
            foreach(var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                rolesDistribution[role.Name] = usersInRole.Count;
            }

            // Actividad reciente
            var recentActivity = await _context.LogAuditoria
                .OrderByDescending(l => l.Timestamp)
                .Take(5)
                .Select(l => new {
                    l.ActionType,
                    l.Timestamp,
                    l.Details,
                    l.UserId
                })
                .ToListAsync();

            return Ok(new
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                InactiveUsers = inactiveUsers,
                RolesDistribution = rolesDistribution,
                RecentActivity = recentActivity
            });
        }
    }
}
