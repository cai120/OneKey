﻿using OneKey.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Domain
{
    public class PasswordDTO : BaseDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string StoredPassword { get; set; }
        public string DetailsFor { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string UserReference { get; set; }
    }
}
