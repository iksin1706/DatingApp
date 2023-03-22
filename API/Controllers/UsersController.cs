using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private readonly DataContext _context;
        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
           var users = await _userRepository.GetMembersAsync();

           var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

           return Ok(usersToReturn);

        }
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user =  await _userRepository.GetMemberByUsenameAsync(username);

            return _mapper.Map<MemberDto>(user);
        }
    }
}