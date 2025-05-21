using Application.DTO;
using Application.Interfaces;
using Auth.Application.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Controllers
{
    [Route("api/userauth")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;

        public UserAuthenticationController(IUserService userService, IValidator<RegisterUserRequest> registerValidator,
            IValidator<LoginRequest> loginValidator)
        {
            _userService = userService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var validationResult = await _registerValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new CustomResponse((int)StatusCodes.Status400BadRequest, "validation error ",validationResult.Errors));
            }

            var result = await _userService.RegisterAsync(request);
            if (!result.Succeeded)
            {
                return BadRequest(new CustomResponse((int)StatusCodes.Status500InternalServerError, "User creation failed.",
                    result.Errors.Select(e => e.Description).ToList()));
            }
            return Ok(new CustomResponse((int)StatusCodes.Status200OK, "Registration successful.",null));
            
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.LoginAsync(request);
            if(result is null || result.Description is not null)
            {
                return BadRequest(new CustomResponse((int)StatusCodes.Status400BadRequest, result.Description));
            }
            
            return Ok(new CustomResponse((int)StatusCodes.Status200OK,"Login successful.",result));
            
        }
    }
}
