// Controllers/DashboardController.cs (Dashboard Summary)
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Services;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IMaterialService _materialService;
        private readonly IPaymentService _paymentService;

        public DashboardController(
            IProjectService projectService,
            IUserService userService,
            IMaterialService materialService,
            IPaymentService paymentService)
        {
            _projectService = projectService;
            _userService = userService;
            _materialService = materialService;
            _paymentService = paymentService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<object>> GetDashboardSummary()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var users = await _userService.GetAllUsersAsync();
            var lowStock = await _materialService.GetLowStockMaterialsAsync();
            var overdue = await _paymentService.GetOverduePaymentsAsync();

            return Ok(new
            {
                TotalProjects = projects.Count,
                ActiveProjects = projects.Count(p => p.Status == "InProgress"),
                TotalUsers = users.Count,
                ActiveUsers = users.Count(u => u.IsActive),
                LowStockMaterials = lowStock.Count,
                OverduePayments = overdue.Count,
                TotalPendingAmount = overdue.Sum(p => p.Amount)
            });
        }
    }
}
