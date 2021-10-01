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
                //return Redirect("~/Tabla/");
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }


        }
    }
}