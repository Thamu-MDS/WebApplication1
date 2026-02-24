// Controllers/MaterialsController.cs
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Services;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaterialDto>>> GetMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDto>> GetMaterial(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        [HttpPost]
        public async Task<ActionResult<MaterialDto>> CreateMaterial(MaterialDto materialDto)
        {
            var createdMaterial = await _materialService.CreateMaterialAsync(materialDto);
            return CreatedAtAction(nameof(GetMaterial), new { id = createdMaterial.Id }, createdMaterial);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaterialDto>> UpdateMaterial(int id, MaterialDto materialDto)
        {
            try
            {
                materialDto.Id = id;
                var updatedMaterial = await _materialService.UpdateMaterialAsync(id, materialDto);
                return Ok(updatedMaterial);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var result = await _materialService.DeleteMaterialAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<List<MaterialDto>>> GetLowStockMaterials()
        {
            var materials = await _materialService.GetLowStockMaterialsAsync();
            return Ok(materials);
        }
    }
}
