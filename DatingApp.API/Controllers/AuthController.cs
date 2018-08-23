using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.UserDto;
using DatingApp.Common.Interfaces.IServices;
using DatingApp.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("auth/[controller]")]
    public class Auth2ServController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IConfiguration _config;

        public Auth2ServController(IAuthService auth, IConfiguration config)
        {
            _auth = auth;
           _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _auth.CheckIfUserValid(user.Name, user.Password);

                if (currentUser==null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "wrong user or password");
                 
                var claims = new[] {

                    new Claim(ClaimTypes.NameIdentifier , currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Name , currentUser.UserName)
 
                };
                 
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Key").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                   Subject= new ClaimsIdentity(claims),
                   Expires =DateTime.Now.AddHours(1),
                   SigningCredentials = creds
                    

                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
              

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)

                });



            }
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _auth.GetUser(user.Name);

                if (currentUser != null)
                    return BadRequest("User already exist");

                if (_auth.RegisterUser(user.Name, user.Password))
                    return Ok();

                return StatusCode(201);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }
    }
}