using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlInventario.Data;
using ControlInventario.Models;

namespace ControlInventario.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProductosController : Controller
    {
        private ControlInventarioContext db = new ControlInventarioContext();

        // GET: Productos
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Empresa).Include(c => c.Categorias);
            var prod = productos.ToList();
            return View(prod);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            var categorias = db.Categorias.ToList();
            var modelo = new ProductoViewModel
            {
                CategoriasDisponibles = categorias.Select(c => new SelectListItem
                {
                    Value = c.CategoriaId.ToString(),
                    Text = c.Nombre
                }).ToList()
            };

            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre");
            return View(modelo);
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Caracteristicas,Precio,EmpresaNIT")] Producto producto, int[] categoriasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                if (categoriasSeleccionadas != null)
                {
                    producto.Categorias = new List<Categoria>();
                    foreach (var categoriaId in categoriasSeleccionadas)
                    {
                        var categoria = db.Categorias.Find(categoriaId);
                        producto.Categorias.Add(categoria);
                    }
                }
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre", producto.EmpresaNIT);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            var categorias = db.Categorias.ToList();
            var modelo = new ProductoViewModel
            {
                CategoriasDisponibles = categorias.Select(c => new SelectListItem
                {
                    Value = c.CategoriaId.ToString(),
                    Text = c.Nombre
                }).ToList()
            };
            modelo.Codigo = producto.Codigo;
            modelo.Nombre = producto.Nombre;
            modelo.Caracteristicas = producto.Caracteristicas;
            modelo.Precio = producto.Precio;
            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre", producto.EmpresaNIT);

            return View(modelo);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Caracteristicas,Precio,EmpresaNIT")] Producto producto, int[] categoriasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                if (categoriasSeleccionadas != null)
                {
                    producto.Categorias = new List<Categoria>();
                    foreach (var categoriaId in categoriasSeleccionadas)
                    {
                        var categoria = db.Categorias.Find(categoriaId);
                        producto.Categorias.Add(categoria);
                    }
                }
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre", producto.EmpresaNIT);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
