using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DALProyectoCRUD;

namespace WAProyectoCrud.Controllers
{
    public class ContactosController : Controller
    {
        
        BTLProyectoCRUD.Servicios.ServicioContactos negContactos = new BTLProyectoCRUD.Servicios.ServicioContactos();

        
        DBPRUEBAS db = new DBPRUEBAS();

        // GET: Contactos
        public ActionResult Index()
        {
            var resultado = negContactos.Listar();

            if (resultado.EsExito)
            {
                
                return View((List<DALProyectoCRUD.CONTACTO>)resultado.Contenido);
            }
            else
            {
                
                ViewBag.Error = resultado.ExcepcionInterna.Message;
                return View(new List<CONTACTO>());
            }
        }

        // GET: Contactos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resultado = negContactos.Buscar((int)id);

            if (resultado.EsExito)
            {
                
                return View((CONTACTO)resultado.Contenido);
            }
            else
            {
                
                ViewBag.Error = resultado.ExcepcionInterna.Message;
                return View(new CONTACTO());
            }
        }

        // GET: Contactos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contactos/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdContacto,Nombre,Telefono,FechaNacimiento,FechaRegistro")] CONTACTO contacto)
        {
            if (ModelState.IsValid)
            {
                var resultado = negContactos.Agregar(contacto.Nombre, contacto.Telefono, 
                    contacto.FechaNacimiento == null ? DateTime.Now : Convert.ToDateTime(contacto.FechaNacimiento), 
                    Convert.ToDateTime(contacto.FechaRegistro)
                    );

                if (resultado.EsExito)
                {
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    ViewBag.Error = resultado.ExcepcionInterna.Message;
                    return View(contacto);
                }
            }

            return View(contacto);
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resultado = negContactos.Buscar(id.Value);

            if (resultado.EsExito)
            {
                
                return View((CONTACTO)resultado.Contenido);
            }
            else
            {
                
                ViewBag.Error = resultado.ExcepcionInterna.Message;
                return View(new CONTACTO());
            }
        }

        // POST: Contactos/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdContacto,Nombre,Telefono,FechaNacimiento,FechaRegistro")] CONTACTO contacto)
        {
            if (ModelState.IsValid)
            {
                var resultado = negContactos.Modificar(contacto.IdContacto, contacto.Nombre, contacto.Telefono, contacto.FechaNacimiento == null ? DateTime.Now : Convert.ToDateTime(contacto.FechaNacimiento),
                    Convert.ToDateTime(contacto.FechaRegistro));

                if (resultado.EsExito)
                {
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    ViewBag.Error = resultado.Mensaje;
                    return View(contacto);
                }
            }
            return View(contacto);
        }

        // GET: Contactos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resultado = negContactos.Buscar(id.Value);

            if (resultado.EsExito)
            {
                
                return View((CONTACTO)resultado.Contenido);
            }
            else
            {
                
                ViewBag.Error = resultado.ExcepcionInterna.Message;
                return View(new CONTACTO());
            }
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var resultado = negContactos.Eliminar(id);

            if (resultado.EsExito)
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                
                ViewBag.Error = resultado.ExcepcionInterna.Message;
                return View(new CONTACTO());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
