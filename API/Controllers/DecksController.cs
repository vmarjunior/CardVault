using CardVault.API.Extensions;
using CardVault.Application.DTOs.Deck;
using CardVault.Application.Interfaces;
using CardVault.Application.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardVault.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController(IDeckService deckService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DeckQueryParameters queryParams)
        {
            var pagedResult = await deckService.GetAllAsync(queryParams);
            return Ok(pagedResult);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var deck = await deckService.GetByIdAsync(id);

            if (deck == null)
                return NotFound();

            return Ok(deck);
        }

        [HttpGet("Users/{id:guid}")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var decks = await deckService.GetByUserIdAsync(id);

            if (decks == null)
                return NotFound();

            return Ok(decks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeckCreateDTO deckCreateDTO)
        {
            var userId = User.GetUserId();
            var deck = await deckService.CreateAsync(userId, deckCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = deck.Id }, deck);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DeckUpdateDTO deckUpdateDTO)
        {
            var userId = User.GetUserId();
            await deckService.UpdateAsync(userId, id, deckUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            await deckService.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}
