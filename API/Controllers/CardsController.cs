using CardVault.Application.Interfaces;
using CardVault.Application.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardVault.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController(ICardService cardService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CardQueryParameters queryParams)
        {
            var pagedResult = await cardService.GetAllAsync(queryParams);
            return Ok(pagedResult);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var card = await cardService.GetByIdAsync(id);

            if (card == null)
                return NotFound();

            return Ok(card);
        }

        [HttpPut("{id:guid}/Price")]
        public async Task<IActionResult> UpdateCardPrice(Guid id, decimal price)
        {
            await cardService.UpdateCardPriceAsync(id, price);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await cardService.DeleteAsync(id);
            return NoContent();
        }
    }
}
