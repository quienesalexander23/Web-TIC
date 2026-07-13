using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using WebTIC.API.Models;
using System;
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

            // Obtener roles y conteo en una sola consulta agrupada (evita el patrón N+1
            // de hacer un GetUsersInRoleAsync por cada rol, que era la causa de la
            // latencia real medida de 1.3-4.5s contra Supabase).
            var roles = await _roleManager.Roles.ToListAsync();
            var roleCounts = await _context.UserRoles
                .GroupBy(ur => ur.RoleId)
                .Select(g => new { RoleId = g.Key, Count = g.Count() })
                .ToListAsync();

            var rolesDistribution = new Dictionary<string, int>();
            foreach (var role in roles)
            {
                var count = roleCounts.FirstOrDefault(rc => rc.RoleId == role.Id)?.Count ?? 0;
                rolesDistribution[role.Name!] = count;
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

            // Actividad de los últimos 7 días (agrupada por día) para la gráfica real del
            // dashboard — antes no existía ninguna serie temporal, solo la lista de
            // "actividad reciente"; esto cierra el hallazgo de la Figura 2.14 de la tesis.
            var sevenDaysAgo = DateTime.UtcNow.Date.AddDays(-6);
            var recentTimestamps = await _context.LogAuditoria
                .Where(l => l.Timestamp >= sevenDaysAgo)
                .Select(l => l.Timestamp)
                .ToListAsync();

            var activityByDay = Enumerable.Range(0, 7)
                .Select(offset => sevenDaysAgo.AddDays(offset))
                .Select(day => new
                {
                    Date = day.ToString("yyyy-MM-dd"),
                    Count = recentTimestamps.Count(t => t.Date == day)
                })
                .ToList();

            return Ok(new
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                InactiveUsers = inactiveUsers,
                RolesDistribution = rolesDistribution,
                RecentActivity = recentActivity,
                ActivityByDay = activityByDay
            });
        }
    }
}
