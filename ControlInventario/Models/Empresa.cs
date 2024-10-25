using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventario.Models
{
    public class Empresa
    {
        [Key]
        public string NIT { get; set; }  // Llave primaria
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // Relación con productos
        public virtual ICollection<Producto> Productos { get; set; }

        //Relación con clientes
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}