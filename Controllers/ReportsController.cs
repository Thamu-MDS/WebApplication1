// Controllers/ReportsController.cs (Reports & Analytics)
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Services;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IProjectService _projectService;

        public ReportsController(IPaymentService paymentService, IProjectService projectService)
        {
            _paymentService = paymentService;
            _projectService = projectService;
        }

        [HttpGet("project-expenses/{projectId}")]
        public async Task<ActionResult<object>> GetProjectExpenses(int projectId)
        {
            var payments = await _paymentService.GetPaymentsByProjectIdAsync(projectId);
            var project = await _projectService.GetProjectByIdAsync(projectId);

            var summary = new
            {
                ProjectId = projectId,
                ProjectName = project?.Name,
                TotalWorkerPayments = payments.Where(p => p.Type == "Worker").Sum(p => p.Amount),
                TotalMaterialPayments = payments.Where(p => p.Type == "Material").Sum(p => p.Amount),
                TotalExpenses = payments.Sum(p => p.Amount),
                PaidAmount = payments.Where(p => p.Status == "Paid").Sum(p => p.Amount),
                PendingAmount = payments.Where(p => p.Status != "Paid").Sum(p => p.Amount)
            };

            return Ok(summary);
        }
    }
}
