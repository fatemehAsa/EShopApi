﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EShopApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace EShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody]Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The Entries Is not Valid");
            }

            if (login.UserName.ToLower()!="iman"||login.Password.ToLower()!="123")
            {
                return Unauthorized("کاربری یافت نشد");
            }
            
            var secretKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ourVerifyToplearn"));
            var signingCredentials=new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var tokenOption=new JwtSecurityToken(
                
                issuer: "http://localhost:38469",
                claims:new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,
                         login.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                },expires:DateTime.Now.AddMinutes(30), signingCredentials:signingCredentials);

            var tokenString=new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new {token = tokenString});
        }
    }
}
