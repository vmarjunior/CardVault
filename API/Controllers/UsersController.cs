using CardVault.API.Extensions;
using CardVault.Application.DTOs.User;
using CardVault.Application.Interfaces;
using CardVault.Application.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardVault.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserQueryParameters queryParams)
        {
            var pagedResult = await userService.GetAllAsync(queryParams);
            return Ok(pagedResult);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userCreateDTO)
        {
            var user = await userService.CreateAsync(userCreateDTO);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPatch("{id:guid}/profile")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UserUpdateProfileDTO userUpdateDTO)
        {
            var userId = User.GetUserId();
            await userService.UpdateProfileAsync(userId, id, userUpdateDTO);
            return NoContent();
        }

        [HttpPut("{id:guid}/account")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UserUpdateAccountDTO userUpdateAccountDTO)
        {
            var userId = User.GetUserId();
            await userService.UpdateAccountAsync(userId, id, userUpdateAccountDTO);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            await userService.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}