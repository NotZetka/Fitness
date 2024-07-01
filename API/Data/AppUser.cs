﻿using Microsoft.AspNetCore.Identity;

namespace API.Database
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
    }
}
