namespace GestionAcademica.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    class DocumentType
    {
        [Key]
        public string DocumentTypeID { get;}

       [Required]
       [Display(Name="Tipo Documento")]
       [StringLength(50)]
        private string name { get; set; }
        
        private string description{ get; set;}
    }

}

