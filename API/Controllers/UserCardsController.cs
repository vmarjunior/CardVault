using CardVault.API.Extensions;
using CardVault.Application.DTOs.UserCard;
using CardVault.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CardVault.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserCardsController(IUserCardService userCardService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await userCardService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> ScanCard([FromBody] CardScanDTO cardScanDTO)
        {
            var userId = User.GetUserId();
            var result = await userCardService.ScanCardAsync(userId, cardScanDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPatch("{id:guid}/virtual-status")]
        public async Task<IActionResult> UpdateVirtualStatus(Guid id)
        {
            var userId = User.GetUserId();
            await userCardService.UpdateCardVirtualStatusAsync(userId, id);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            await userCardService.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}