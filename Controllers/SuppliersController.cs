// Controllers/SuppliersController.cs
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Services;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierDto>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> CreateSupplier(SupplierDto supplierDto)
        {
            var createdSupplier = await _supplierService.CreateSupplierAsync(supplierDto);
            return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.Id }, createdSupplier);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierDto>> UpdateSupplier(int id, SupplierDto supplierDto)
        {
            try
            {
                supplierDto.Id = id;
                var updatedSupplier = await _supplierService.UpdateSupplierAsync(id, supplierDto);
                return Ok(updatedSupplier);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplierAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
