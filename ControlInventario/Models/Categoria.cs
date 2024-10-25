using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventario.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

    }
}