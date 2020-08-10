using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace GestionAcademica.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }

        public RoleEdit()
        {
        }
    }
}
