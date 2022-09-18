using CosmosCRUD.DTOs;
using CosmosCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Mime;

namespace CosmosCRUD.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponseDTO))]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO usersDTO)
        {
            UserResponseDTO response = await usersService.CreateUser(usersDTO);
            return Created("CreatedUser", response);
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseDTO))]
        public async Task<IActionResult> GetUserByEmailAddress([BindRequired, FromQuery] string emailAddress)
        {
            UserResponseDTO response = await usersService.GetUser(emailAddress);
            return Ok(response);
        }
    }
}
