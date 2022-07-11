using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarea1.Models;

namespace Tarea1.Controllers
{
    public class HomeController : Controller
    {
        private Tarea1.Models.Tarea1Entities1 contextoDatos = new Tarea1Entities1();
            

        public ActionResult Index()
        {
            //cargamos los productos como una lista y lo pasamos a la vista
            var productos = contextoDatos.Producto.ToList();
            
            return View(productos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Detalle(int id)
        {

            var prod=(from p in contextoDatos.Producto where p.Id==id select p).FirstOrDefault();

            if(prod!=null)
                return View(prod);
            else
                return RedirectToAction("Index");
        }
       
    }
}