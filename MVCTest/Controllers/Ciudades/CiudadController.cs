using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;
using MVCTest.Models.CiudadModel;

namespace MVCTest.Controllers.Ciudades
{
    public class CiudadController : Controller
    {
        // GET: Ciudad

        /*
            indice inicial para mostrar el listado de ciudades
        */
        public ActionResult Index()
        {
            List<Ciudadvm> lst;
            using (PruebaMVCEntities db = new PruebaMVCEntities())
            {
                lst = (from d in db.Tblciudad
                       select new Ciudadvm
                       {
                           ID = d.ID,
                           Nombre = d.Nombre,
                       }).ToList();
            }

            return View(lst);
        }

        #region Cities CRUD operations
        public ActionResult Nuevaciudad()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Nuevaciudad(Ciudadvm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PruebaMVCEntities db = new PruebaMVCEntities())
                    {
                        var ciu = new Tblciudad();
                        ciu.Nombre = model.Nombre;

                        db.Tblciudad.Add(ciu);
                        db.SaveChanges();
                    }

                    return Redirect("/Ciudad");
                }

                return View(model);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editarciudad(int ID)
        {

            Ciudadvm model = new Ciudadvm();
            using (PruebaMVCEntities db = new PruebaMVCEntities())
            {
                Tblciudad ciu = db.Tblciudad.Find(ID);

                model.Nombre = ciu.Nombre;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editarciudad(Ciudadvm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PruebaMVCEntities db = new PruebaMVCEntities())
                    {
                        var ciu = db.Tblciudad.Find(model.ID); ;
                        ciu.Nombre = model.Nombre;
                        

                        db.Entry(ciu).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("/Ciudad");
                }

                return View(model);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}