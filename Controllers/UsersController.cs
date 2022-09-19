using CosmosCRUD.DTOs;
using CosmosCRUD.Exceptions;
using CosmosCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO usersDTO)
        {
            try
            {
                UserResponseDTO response = await usersService.CreateUser(usersDTO);
                return Created("CreatedUser", response);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetUserByEmailAddress([Required] [FromQuery] string emailAddress)
        {
            try
            {
                UserResponseDTO response = await usersService.GetUser(emailAddress);
                return Ok(response);
            } catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
