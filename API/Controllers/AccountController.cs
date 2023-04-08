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
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(
            DataContext context,
            ITokenService tokenService,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._context = context;
            this._tokenService = tokenService;
        }

        [HttpPost("register")] //POST api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
        {
            if (await UserExist(registerDTO.UserName))
                return BadRequest("User with that username already exists");

            var user = _mapper.Map<AppUser>(registerDTO);

            using var hmac = new HMACSHA512();
            user.UserName = registerDTO.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(registerDTO.Password.ToLower())
            );
            user.PasswordSalt = hmac.Key;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.isMain)?.Url,
                KnownAs = user.KnownAs
            };
        }

        public async Task<bool> UserExist(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);

            if (user == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.isMain)?.Url
            };
        }
    }
}
