// Services/IPaymentService.cs
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<List<PaymentDto>> GetPaymentsByProjectIdAsync(int projectId);
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
        Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto);
        Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto);
        Task<bool> DeletePaymentAsync(int id);
        Task<List<PaymentDto>> GetOverduePaymentsAsync();
    }
}
