using CardVault.Application.Constants;
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

        [HttpGet("{id}", Name = nameof(GetById))]
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

        [HttpPatch("update/profile/{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UserUpdateProfileDTO userUpdateDTO)
        {
            await userService.UpdateProfileAsync(id, userUpdateDTO);
            return NoContent();
        }

        [HttpPatch("update/account/{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UserUpdateAccountDTO userUpdateAccountDTO)
        {
            await userService.UpdateAccountAsync(id, userUpdateAccountDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await userService.DeleteAsync(id);
            return NoContent();
        }
    }
}