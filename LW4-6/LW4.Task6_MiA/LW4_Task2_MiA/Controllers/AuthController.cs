using AutoMapper;
using LW4_Task2_MiA.Controllers;
using LW4_Task2_MiA.Models;
using LW4_Task4_MiA.DTO;
using LW4_Task4_MiA.Service;
using LW4_Task6_MiA.DTO;
using LW4_Task6_MiA.Service;
using LW4_Task6_MiA.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LW4_Task6_MiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly int _refreshTokenExpirationDays;

        public AuthController(IUserService userService, ITokenService tokenService, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _refreshTokenExpirationDays = jwtSettings.Value.RefreshTokenExpirationDays;
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userDto = await _userService.GetByIdAsync(userId);

            if (userDto == null)
            {
                return NotFound("Користувача, пов'язаного з цим токеном, не знайдено.");
            }

            return Ok(userDto);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequestDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdUser = await _userService.RegisterAsync(registerDto);

            if (createdUser == null)
            {
                return BadRequest(new { message = "Цей email вже використовується." });
            }

            return CreatedAtAction(
                nameof(UsersController.GetById), 
                "Users",
                new { id = createdUser.Id }, 
                createdUser);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.ValidateAndGetUserAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Невірний email або пароль.");
            }

            var accessToken = _tokenService.CreateAccessToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);

            await _userService.UpdateAsync(user.Id!, _mapper.Map<UserDto>(user));

            return Ok(new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponseDto>> Refresh([FromBody] TokenResponseDto tokenDto)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            if (principal?.Identity?.Name is null)
            {
                return Unauthorized("Недійсний access token.");
            }

            var userId = principal.Identity.Name;
            var userDto = await _userService.GetByIdAsync(userId);
            var user = _mapper.Map<User>(userDto);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized("Недійсний refresh token.");
            }

            var newAccessToken = _tokenService.CreateAccessToken(user);
            var newRefreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);
            await _userService.UpdateAsync(user.Id!, _mapper.Map<UserDto>(user));

            return Ok(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
