using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace GestionAcademica.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "Tipo de Documento")]
        public int DocumentType { get; set; }

        [Required]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [MaxLength(180)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        [MaxLength(140)]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Genero")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(255)]
        public string Address { get; set; }

        [Display(Name = "Telefono")]
        [MaxLength(8)]
        public string Phone { get; set; } 

        [Display(Name = "Celular")]
        [MaxLength(11)]
        public string Telephone { get; set; }

        
    }
}
