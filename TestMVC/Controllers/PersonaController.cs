using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class PersonaController : Controller

    {
        private Models.PruebaMVCContext _db;

        public PersonaController(PruebaMVCContext db)
        {
            _db = db;
        }

        private void ciudades()
        {
            var lst = _db.Tblciudads.ToList();
            List<SelectListItem> it = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString()
                };
            });

            ViewBag.items = it;
        }

        // GET: Persona
        public ActionResult Index()
        {
            var lst = _db.Tblpersonas.ToList();

            return View(lst);
        }

        // GET: Persona/Create
        public ActionResult NuevaPersona()
        {
            ciudades();
            
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaPersona(Tblpersona model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var per = new Tblpersona();
                    per.Nombre = model.Nombre;
                    per.Apellido = model.Apellido;
                    per.IdCiudad = model.IdCiudad;
                    per.Telefono = model.Telefono;
                    per.Edad = model.Edad;
                    per.Activo = model.Activo;

                    _db.Tblpersonas.Add(per);
                    _db.SaveChanges();
                }

                return Redirect("/Persona");

            }
            catch
            {
                return ViewBag("Error");
            }
        }

        // GET: Persona/Edit/5
        public ActionResult EditarPersona(int id)
        {
            ciudades();
            var model = new Tblpersona();
            var per = _db.Tblpersonas.Find(id);

            model.Nombre = per.Nombre;
            model.Apellido = per.Apellido;
            model.IdCiudad = per.IdCiudad;
            model.Telefono = per.Telefono;
            model.Edad = per.Edad;
            model.Activo = (bool)per.Activo;

            return View(model);
        }

        // POST: Persona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Tblpersona model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var per = _db.Tblpersonas.Find(model.Id);
                    per.Nombre = model.Nombre;
                    per.Apellido = model.Apellido;
                    per.IdCiudad = model.IdCiudad;
                    per.Telefono = model.Telefono;
                    per.Edad = model.Edad;
                    per.Activo = model.Activo;

                    _db.Entry(per).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                }

                return Redirect("/Persona");

            }
            catch
            {
                return ViewBag("Error");
            }
        }

        // POST: Persona/Delete/5
        [HttpGet]
        public ActionResult EliminarPersona(Tblpersona model)
        {
            var per = _db.Tblpersonas.Find(model.Id);
            _db.Tblpersonas.Remove(per);
            _db.SaveChanges();

            return Redirect("/Persona");
        }
    }
}
