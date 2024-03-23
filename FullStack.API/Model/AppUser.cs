﻿using Microsoft.AspNetCore.Identity;

namespace FullStack.API.Model
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }

    }
}