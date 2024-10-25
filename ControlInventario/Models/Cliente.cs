using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventario.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        // Relación con órdenes
        public virtual ICollection<Orden> Ordenes { get; set; }

        // Relación con empresas
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}