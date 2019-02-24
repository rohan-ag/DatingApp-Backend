using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExits(userForRegisterDto.Username))
                return BadRequest("User already exists");

            var user = new User
            {
                Username = userForRegisterDto.Username
            };
            var createdUser = await _repo.Register(user, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}
