using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCrud.Models;
using MVCrud.Models.ViewModels;


namespace MVCrud.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lista = new List<ListTablaViewModel>();

            using ( CieloEntities db = new CieloEntities() )
            {
                lista = (from d in db.Persona
                         select new ListTablaViewModel
                         {
                             Id = d.Id,
                             Nombre = d.Nombre,
                             Apellido = d.Apellido,
                             FechaNacimiento = (DateTime)d.FechaNacimiento
                         }).ToList();
            }

            return View(lista);  
        }

        public ActionResult Nuevo()
        {
            return View();
        }  

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if( ModelState.IsValid )
                {
                    using (CieloEntities db = new CieloEntities())
                    {
                        var oTabla = new Persona();

                        oTabla.Nombre = model.Nombre;
                        oTabla.Apellido = model.Apellido;
                        oTabla.FechaNacimiento = DateTime.Now;

                        db.Persona.Add(oTabla);
                        db.SaveChanges();

                    }
                    return Redirect("~/Tabla/Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }


        }


        public ActionResult Editar(int Id)
        {
            TablaViewModel modelo = new TablaViewModel();
            using (CieloEntities db = new CieloEntities())
            {
                var item = db.Persona.Find(Id);
                modelo.Nombre = item.Nombre;
                modelo.Apellido = item.Apellido;
                modelo.FechaNacimiento = (DateTime) item.FechaNacimiento;
                modelo.Id = item.Id;
                
            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CieloEntities db = new CieloEntities())
                    {
                        var oTabla = db.Persona.Find(model.Id);

                        oTabla.Nombre = model.Nombre;
                        oTabla.Apellido = model.Apellido;
                        oTabla.FechaNacimiento = DateTime.Now;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Redirect("~/Tabla/Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            using (CieloEntities db = new CieloEntities())
            {
                var item = db.Persona.Find(Id);
                db.Persona.Remove(item);
                db.SaveChanges();

            }
            return Redirect("~/Tabla/Index");
        }
    }
}