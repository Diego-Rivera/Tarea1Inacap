using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarea1.Models;

namespace Tarea1.Controllers
{
    public class ProductosController : Controller
    {
        Models.Tarea1Entities1 contexto = new Models.Tarea1Entities1();
        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Validar()
        {
            //Capturar los  valores del formulario
            var email = Request["email"];
            var pass = Request["password"];
            //Buscar los datos en la BD
            var usu = (from u in contexto.Usuario where u.email == email && u.pass == pass select u).FirstOrDefault();
            //Si existen Crear session con los datos
            if(usu!=null)
            {
                Session["Usuario"] = usu;
                 return RedirectToAction("Listar");
            }
            else
            {
                return RedirectToAction("Index");
            }
            //Si no redirecciono al Index con un mensaje
            
        }

        public ActionResult Listar()
        {
            if(Session["Usuario"]!=null)
            {
                var prod = contexto.Producto.ToList();
                var cat = contexto.CategoriaProductos.ToList();
                ViewBag.Categorias = cat;
                return View(prod);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            if (Session["Usuario"] != null)
            {
                var prod = (from p in contexto.Producto where p.Id == id select p).FirstOrDefault();

                contexto.Producto.Remove(prod);

                contexto.SaveChanges();

                return RedirectToAction("Listar");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Guardar()
        {
            string mensaje = "";
            var id = Request["idProducto"];
            var nombre = Request["nombre"];
            var descripcion = Request["descripcion"];
            var precio = Request["precio"];
            var stock = Request["stock"];
            var idCategoria = Request["idCategoria"];


            Producto p = new Producto();
            p.Id = int.Parse(id);
            p.Nombre = nombre;
            p.Descripcion = descripcion;
            p.Precio = int.Parse(precio);
            p.Stock = int.Parse(stock);
            p.IdCategoria = int.Parse(idCategoria);

            
            try
            {
                contexto.Producto.Add(p);
                contexto.SaveChanges();
                mensaje = "Guardado con exito";
            }
            catch(Exception ex)
            {
                mensaje = "Error al guardar el dato";
            }
            ViewBag.mensaje = mensaje;

            return RedirectToAction("Listar");
        }

        public ActionResult MostrarModificar(int id)
        {
            var prod = (from p in contexto.Producto where p.Id == id select p).FirstOrDefault();


            if (prod != null)
            {
                var cat = contexto.CategoriaProductos.ToList();
                ViewBag.Categorias = cat;
                return View(prod);
            }
            else
                return RedirectToAction("Listar");
        }

        public ActionResult GuardarModificar()
        {
            int id = int.Parse(Request["idProducto"]);
            var nombre = Request["nombre"];
            var descripcion = Request["descripcion"];
            var precio = Request["precio"];
            var stock = Request["stock"];
            var idCategoria = Request["idCategoria"];


            Producto p = (from pro in contexto.Producto where pro.Id == id select pro).FirstOrDefault();
            if(p!=null)
            {
                p.Nombre = nombre;
                p.Descripcion = descripcion;
                p.Precio = int.Parse(precio);
                p.Stock = int.Parse(stock);
                p.IdCategoria = int.Parse(idCategoria);

                contexto.SaveChanges();
            }
            
            return RedirectToAction("Listar");
        }
    }
}