using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTest.Models.CiudadModel
{
    public class Ciudadvm
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre ciudad")]
        public string Nombre { get; set; }
    }
}