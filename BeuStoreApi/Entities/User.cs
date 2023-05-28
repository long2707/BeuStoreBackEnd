﻿using Microsoft.AspNetCore.Identity;

namespace BeuStoreApi.Entities
{
    public class User: IdentityUser
    {
        public string firstName {  get; set; }= string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string? profileImage { get; set; }
    }
}
