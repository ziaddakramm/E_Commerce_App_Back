using e_commerce_api.Api;
using e_commerce_api.Api.Dtos;
using e_commerce_api.Entities;
using e_commerce_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Collections.Specialized;
using System.Diagnostics;

namespace e_commerce_api.Controllers
{
    [Authorize]
    [ApiController]
    //Defining which Http route this controller will receive from
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;
        public UsersController(IJwtAuth jwtAuth)
        {
            //Auth initialization
            this.jwtAuth = jwtAuth;
        }



        [AllowAnonymous]
        //POST /items
        [HttpPost("{authentication}")]
        public IActionResult Authenticate(UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return BadRequest(new { token = "Invalid credentials" });
            AuthenticateResponse response=new AuthenticateResponse(userCredential,token);
              return Ok(response);
        }
    }
}