using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlInventario.Models
{
    public class ProductoViewModel 
    {
        public int Codigo { get; set; } // Llave primaria
        public string Nombre { get; set; }
        public string Caracteristicas { get; set; }
        public decimal Precio { get; set; }

        // Relación con la empresa
        public string EmpresaNIT { get; set; }
        public virtual Empresa Empresa { get; set; }

        // Relación con categorías
        public virtual ICollection<Categoria> Categorias { get; set; }
        public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; }

        public List<SelectListItem> CategoriasDisponibles { get; set; }
    }
}