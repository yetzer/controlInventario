using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlInventario.Data;
using ControlInventario.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

namespace ControlInventario.Controllers
{
    [Authorize]
    public class InventarioController : Controller
    {
        private ControlInventarioContext db = new ControlInventarioContext();

        // GET: Inventario
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Empresa);
            return View(productos.ToList());
        }

        // GET: Inventario/Details/5
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

        // GET: Inventario/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaNIT = new SelectList(db.Empresas, "NIT", "Nombre");
            return View();
        }

        // POST: Inventario/Create
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

        // GET: Inventario/Edit/5
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

        // POST: Inventario/Edit/5
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

        // GET: Inventario/Delete/5
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

        // POST: Inventario/Delete/5
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

        public ActionResult generarPDF()
        {
            var archivoPDF = DescargarInventarioPDF();
            //byte[] fileBytes = System.IO.File.ReadAllBytes("application/pdf/Inventario.pdf");
            //EnviarCorreo("jose.david.campo@gmail.com", fileBytes);

            return (archivoPDF);

        }

        public FileContentResult DescargarInventarioPDF()
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

                return File(ms.ToArray(), "application/pdf", "Inventario_" + DateTime.Now.TimeOfDay + ".pdf");
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
