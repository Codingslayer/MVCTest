using System;
using System.Collections.Generic;

#nullable disable

namespace TestMVC.Models
{
    public partial class Tblciudad
    {
        public Tblciudad()
        {
            Tblpersonas = new HashSet<Tblpersona>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Tblpersona> Tblpersonas { get; set; }
    }
}
