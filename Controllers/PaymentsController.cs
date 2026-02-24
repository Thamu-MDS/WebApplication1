// Controllers/PaymentsController.cs
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Services;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentDto>>> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<List<PaymentDto>>> GetPaymentsByProject(int projectId)
        {
            var payments = await _paymentService.GetPaymentsByProjectIdAsync(projectId);
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(PaymentDto paymentDto)
        {
            var createdPayment = await _paymentService.CreatePaymentAsync(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, createdPayment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDto>> UpdatePayment(int id, PaymentDto paymentDto)
        {
            try
            {
                paymentDto.Id = id;
                var updatedPayment = await _paymentService.UpdatePaymentAsync(id, paymentDto);
                return Ok(updatedPayment);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<PaymentDto>>> GetOverduePayments()
        {
            var payments = await _paymentService.GetOverduePaymentsAsync();
            return Ok(payments);
        }
    }
}
