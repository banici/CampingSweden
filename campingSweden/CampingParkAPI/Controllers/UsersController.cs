﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampingParkAPI.Models;
using CampingParkAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampingParkAPI.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = _userRepo.Authenticate(model.Username, model.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect." });
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationModel model)
        {
            bool usernameAvailable = _userRepo.isUniqueUser(model.Username);
            if(!usernameAvailable)
            {
                return BadRequest(new { messge = $"The username: {model.Username} is already taken." });
            }

            var user = _userRepo.Register(model.Username, model.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Error occured when registering." });
            }

            return Ok();
        }
    }
}