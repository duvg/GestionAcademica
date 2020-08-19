using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademica.Models
{
    [Table("Categories")]
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El nombre debe tener de 4 a 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no debe exeder los 255 caracteres")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Estado")]
        public Boolean Status { get; set; }

    }
}
