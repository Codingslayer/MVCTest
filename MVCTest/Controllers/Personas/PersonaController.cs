using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCTest.Controllers.Ciudades;

using MVCTest.Models.PersonaModel;

namespace MVCTest.Controllers.Personas
{
    public class PersonaController : Controller
    {
        // objeto Ciudades List
        private IEnumerable<SelectListItem> Ciudadlist;

        /*
            indice inicial para mostrar el listado de personas
        */

        public ActionResult Index()
        {
            List<Personavm> lst;
            using (PruebaMVCEntities db = new PruebaMVCEntities())
            {
                lst = (from p in db.Tblpersona
                       select new Personavm
                       {
                           ID = p.ID,
                           Nombre = p.Nombre,
                           Apellido = p.Apellido,
                           IdCiudad = p.IdCiudad,
                           Telefono = p.Telefono,
                           Edad = p.Edad,
                           Activo = (bool)p.Activo
                       }).ToList();
            }

            return View(lst);
        }

        #region Cities List
        public List<Tblciudad> Consultar()
        {
            using (PruebaMVCEntities db = new PruebaMVCEntities())
            {
                return db.Tblciudad.AsNoTracking().ToList();
            }
        }

        private void listaciudades()
        {
            Ciudadlist = new PersonaController().Consultar().Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Nombre });
        }

        #endregion

        #region Persona CRUD operations
        public ActionResult Nuevapersona()
        {
            listaciudades();
            Personavm modelo = new Personavm()
            {

                Listciudad = Ciudadlist

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevapersona(Personavm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PruebaMVCEntities db = new PruebaMVCEntities())
                    {
                        var per = new Tblpersona();
                        per.Nombre = model.Nombre;
                        per.Apellido = model.Apellido;
                        per.IdCiudad = model.Ciudades.ID;
                        per.Telefono = model.Telefono;
                        per.Edad = model.Edad;
                        per.Activo = model.Activo;

                        db.Tblpersona.Add(per);
                        db.SaveChanges();
                    }

                    return Redirect("/Persona");
                }

                return View(model);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editarpersona(int ID)
        {

            Personavm model = new Personavm();
            using (PruebaMVCEntities db = new PruebaMVCEntities())
            {
                Tblpersona per = db.Tblpersona.Find(ID);

                model.Nombre = per.Nombre;
                model.Apellido = per.Apellido;
                model.Ciudad = per.IdCiudad;
                model.Telefono = per.Telefono;
                model.Edad = per.Edad;
                model.Activo = (bool)per.Activo;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editarpersona(Personavm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PruebaMVCEntities db = new PruebaMVCEntities())
                    {
                        var per = db.Tblpersona.Find(model.ID); ;
                        per.Nombre = model.Nombre;
                        per.Apellido = model.Apellido;
                        per.Telefono = model.Telefono;
                        per.Edad = model.Edad;
                        per.Activo = model.Activo;

                        db.Entry(per).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("/Persona");
                }

                return View(model);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminarpersona(Personavm model)
        {
            using (PruebaMVCEntities db = new PruebaMVCEntities())
            {
                var per = db.Tblpersona.Find(model.ID);
                db.Tblpersona.Remove(per);
                db.SaveChanges();
            }

            return Redirect("/Persona");
        }
        #endregion
    }
}
