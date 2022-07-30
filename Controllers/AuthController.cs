using DotnetCoreWebApiProject.Data;
using DotnetCoreWebApiProject.Models;
using DotnetCoreWebApiProject.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DotnetCoreWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var response = await _authRepository.Register(
                new User { Username = userDto.Username }, userDto.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var response = await _authRepository.Login(userDto.Username, userDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
