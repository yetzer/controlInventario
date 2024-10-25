using ControlInventario.Data;
using ControlInventario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlInventario.Services
{
    public class EmpresasService
    {
        private ControlInventarioContext db = new ControlInventarioContext();
        public EmpresasService() { }

        public Empresa GetEmpresa(string id)
        {
            Empresa empresa = db.Empresas.Find(id);
            return empresa;
        }

        public List<Empresa> GetEmpresas()
        {
            return db.Empresas.ToList();
        }

        public void CreateEmpresa(Empresa empresa)
        {
            db.Empresas.Add(empresa);
            db.SaveChanges();
        }

        public void UpdateEmpresa(Empresa empresa)
        {
            db.Entry(empresa).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteEmpresa(string id) {

            Empresa empresa = GetEmpresa(id);
            if (empresa != null)
            {
                db.Empresas.Remove(empresa);
                db.SaveChanges();
            }

        }

    }
}