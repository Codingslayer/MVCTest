using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;

namespace MVCTest.Models.PersonaModel
{
    public class Personavm
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public int Ciudad { get; set; }

        public int IdCiudad { get; set; }

        public IEnumerable<SelectListItem> Listciudad { get; set; }

        public Tblciudad Ciudades { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public string Edad { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
}