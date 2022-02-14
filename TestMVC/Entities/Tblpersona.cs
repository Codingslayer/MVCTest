using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestMVC.Models
{
    public partial class Tblpersona
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public int IdCiudad { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public string Edad { get; set; }
        public bool Activo { get; set; }

        public virtual Tblciudad IdCiudadNavigation { get; set; }
    }
}
