using System;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademica.Models
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string[] AddIds { get; set; }

        public string[] DeleteIds { get; set; }

        public RoleModification()
        {
        }
    }
}
