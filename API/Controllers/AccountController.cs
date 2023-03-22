using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly DataContext _dataContext;
        public AccountController(DataContext dataContext,ITokenService tokenService)
        {
            this._dataContext = dataContext;
            this._tokenService = tokenService; 
        }
        [HttpPost("register")] //POST api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO){
            if(await UserExist(registerDTO.UserName)) return BadRequest("User with that username already exists");
            
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName=registerDTO.UserName.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password.ToLower())),
                PasswordSalt= hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new  UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<bool> UserExist(string userName){
            return await _context.Users.AnyAsync(x=>x.UserName==userName.ToLower());
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto){
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if(user==null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i]!= user.PasswordHash[i])return Unauthorized("Invalid password");
            }
                return new  UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}