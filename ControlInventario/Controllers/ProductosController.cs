using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ControlInventario.Data;
using ControlInventario.Models;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ControlInventario.Controllers
{
    public class ProductosController : Controller
    {
        private ControlInventarioContext db = new ControlInventarioContext();

        // GET: Productos
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Empresa);
            return View(productos.ToList());
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
            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Caracteristicas,Precio,EmpresaNIT")] Producto producto)
        {
            if (ModelState.IsValid)
            {
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
            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre", producto.EmpresaNIT);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Caracteristicas,Precio,EmpresaNIT")] Producto producto)
        {
            if (ModelState.IsValid)
            {
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

        public ActionResult DescargarInventarioPDF()
        {
            var productos = db.Productos.ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();
                document.Add(new Paragraph("Inventario de Productos"));
                foreach (var producto in productos)
                {
                    document.Add(new Paragraph($"Producto: {producto.Nombre}, Precio: {producto.Precio}"));
                }
                document.Close();
                return File(ms.ToArray(), "application/pdf", "Inventario.pdf");
            }
        }

        public void EnviarCorreo(string emailDestino, byte[] pdf)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(emailDestino);
            mail.Subject = "Inventario de Productos";
            mail.Body = "Adjunto el inventario.";
            mail.Attachments.Add(new Attachment(new MemoryStream(pdf), "Inventario.pdf"));

            SmtpClient smtp = new SmtpClient();
            smtp.Send(mail);
        }

    }
}
