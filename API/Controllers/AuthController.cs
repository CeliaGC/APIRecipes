using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using API.IServices;
using API.Services;
using System;
using Security.IServices;
using Security.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Cors;
using Resources.RequestModels;
using Security.IServices;
using Data;
using Entities.Entities;
using API.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserSecurityService _userSecurityService;
        private readonly ServiceContext _serviceContext;
        public AuthController(IConfiguration configuration, 
                              IUserService usersService, 
                              IUserSecurityService userSecurityService,
                              ServiceContext serviceContext)
        {
            _configuration = configuration;
            _userService = usersService;
            _userSecurityService = userSecurityService;
            _serviceContext = serviceContext;
        }

        [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Verifica las credenciales del usuario (aquí deberías verificar en tu base de datos)
            var usersData = _userService.GetAllUsers();
            UserItem user = usersData.Where(user => user.UserName == request.UserName).First();
            int userIdRol = user.IdRol;
            UserRolItem rol = _serviceContext.Set<UserRolItem>().Where(ur => ur.Id == userIdRol).FirstOrDefault();
            // Genera un token JWT
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        private string GenerateJwtToken(UserItem user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT Secret no está configurado.");
            }
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            //var rolName = _serviceContext.RolType.Find().Name.Where(r )
            var tokenDescriptor = new SecurityTokenDescriptor
           
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, user.IdRol.ToString())
                              //ClaimTypes.Name, )
                    // Otros claims si es necesario
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Duración del token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

