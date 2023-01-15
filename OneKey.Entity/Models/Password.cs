using OneKey.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Entity.Models
{
    public class Password : IBaseEntity
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string DetailsFor { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string StoredPassword { get; set; }
    }
}
