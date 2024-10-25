using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventario.Models
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }

        // Relación con cliente
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        // Relación con productos
        public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; }
    }
}