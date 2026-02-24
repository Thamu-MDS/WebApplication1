// Services/PaymentService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;
using System;

namespace ConstructionManagementSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Select(p => new PaymentDto  // ✅ Projection - NULL SAFE
                {
                    Id = p.id,
                    Type = p.type.ToString(),
                    UserId = p.user_id,
                    UserName = _context.Users.Where(u => u.id == p.user_id).Select(u => u.full_name).FirstOrDefault() ?? "N/A",
                    MaterialId = p.material_id,
                    MaterialName = _context.Materials.Where(m => m.id == p.material_id).Select(m => m.name).FirstOrDefault() ?? "N/A",
                    ProjectId = p.project_id,
                    ProjectName = _context.Projects.Where(pr => pr.id == p.project_id).Select(pr => pr.name).FirstOrDefault() ?? "N/A",
                    PaymentDate = p.payment_date,
                    DueDate = p.due_date,
                    Amount = p.amount,
                    Status = p.status.ToString(),
                    CreatedAt = p.created_at
                })
                .ToListAsync();
        }

        public async Task<List<PaymentDto>> GetPaymentsByProjectIdAsync(int projectId)
        {
            return await _context.Payments
                .Where(p => p.project_id == projectId)
                .Select(p => new PaymentDto
                {
                    Id = p.id,
                    Type = p.type.ToString(),
                    UserId = p.user_id,
                    UserName = _context.Users.Where(u => u.id == p.user_id).Select(u => u.full_name).FirstOrDefault() ?? "N/A",
                    MaterialId = p.material_id,
                    MaterialName = _context.Materials.Where(m => m.id == p.material_id).Select(m => m.name).FirstOrDefault() ?? "N/A",
                    ProjectId = p.project_id,
                    ProjectName = _context.Projects.Where(pr => pr.id == p.project_id).Select(pr => pr.name).FirstOrDefault() ?? "N/A",
                    PaymentDate = p.payment_date,
                    DueDate = p.due_date,
                    Amount = p.amount,
                    Status = p.status.ToString()
                })
                .ToListAsync();
        }

        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.id == id);

            if (payment == null) return null;

            return new PaymentDto
            {
                Id = payment.id,
                Type = payment.type.ToString(),
                UserId = payment.user_id,
                UserName = payment.User?.full_name ?? "N/A",
                MaterialId = payment.material_id,
                MaterialName = payment.Material?.name ?? "N/A",
                ProjectId = payment.project_id,
                ProjectName = payment.Project?.name ?? "N/A",
                PaymentDate = payment.payment_date,
                DueDate = payment.due_date,
                Amount = payment.amount,
                Status = payment.status.ToString(),
                CreatedAt = payment.created_at
            };
        }

        public async Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                type = Enum.Parse<PaymentType>(paymentDto.Type),
                user_id = paymentDto.UserId,
                material_id = paymentDto.MaterialId,
                project_id = paymentDto.ProjectId,
                payment_date = paymentDto.PaymentDate,
                due_date = paymentDto.DueDate,
                amount = paymentDto.Amount,
                status = Enum.Parse<PaymentStatus>(paymentDto.Status)
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            paymentDto.Id = payment.id;
            paymentDto.CreatedAt = payment.created_at;
            return paymentDto;
        }

        public async Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) throw new Exception("Payment not found");

            payment.type = Enum.Parse<PaymentType>(paymentDto.Type);
            payment.user_id = paymentDto.UserId;
            payment.material_id = paymentDto.MaterialId;
            payment.project_id = paymentDto.ProjectId;
            payment.payment_date = paymentDto.PaymentDate;
            payment.due_date = paymentDto.DueDate;
            payment.amount = paymentDto.Amount;
            payment.status = Enum.Parse<PaymentStatus>(paymentDto.Status);

            await _context.SaveChangesAsync();
            paymentDto.Id = id;
            return paymentDto;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PaymentDto>> GetOverduePaymentsAsync()
        {
            return await _context.Payments
                .Where(p => p.status == PaymentStatus.Unpaid &&
                           p.due_date < DateTime.Today &&
                           p.due_date != null)
                .Select(p => new PaymentDto
                {
                    Id = p.id,
                    ProjectId = p.project_id,
                    ProjectName = _context.Projects.Where(pr => pr.id == p.project_id).Select(pr => pr.name).FirstOrDefault() ?? "N/A",
                    DueDate = p.due_date,
                    Amount = p.amount,
                    Status = p.status.ToString(),
                    Type = p.type.ToString()
                })
                .ToListAsync();
        }
    }
}
