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
using ControlInventario.Services;

namespace ControlInventario.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        
        // GET: Empresas
        public ActionResult Index()
        {
            var empresaService = new EmpresasService();
            return View(empresaService.GetEmpresas());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresaService = new EmpresasService();
            Empresa empresa = empresaService.GetEmpresa(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NIT,Nombre,Direccion,Telefono")] Empresa empresa)
        {

            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.CreateEmpresa(empresa);
                return RedirectToAction("Index");
            }

            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var empresaService = new EmpresasService();
            Empresa empresa = empresaService.GetEmpresa(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NIT,Nombre,Direccion,Telefono")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.UpdateEmpresa(empresa);
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var empresaService = new EmpresasService();
            Empresa empresa = empresaService.GetEmpresa(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var empresaService = new EmpresasService();
            empresaService.DeleteEmpresa(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
