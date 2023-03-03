using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.entities
{
    public class AppUser 
    {
        public int Id { get; set; }
        public string  UserName { get; set; }
    }
}