using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAllDiscountsAsync();
            return Ok(discounts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetDiscountByIdAsync(id);
            if (discount == null)
                return NotFound(new { message = "Discount not found" });

            return Ok(discount);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDiscountDto dto)
        {
            try
            {
                var createdDiscount = await _discountService.CreateDiscountAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdDiscount.DiscountId }, createdDiscount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the discount", detail = ex.Message });
            }
        }

        [HttpPatch("{id:int}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var success = await _discountService.ToggleDiscountStatusAsync(id);
            if (!success)
                return NotFound(new { message = "Discount not found" });

            return Ok(new { message = "Discount status updated successfully" });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _discountService.DeleteDiscountAsync(id);
            if (!success)
                return NotFound(new { message = "Discount not found" });

            return NoContent();
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestActiveDiscount()
        {
            var discount = await _discountService.GetLatestActiveDiscountAsync();

            if (discount == null)
                return NotFound("No active discounts found.");

            return Ok(discount);
        }
    }    
    }

