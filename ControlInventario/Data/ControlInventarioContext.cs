using ControlInventario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlInventario.Data
{
    public class ControlInventarioContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ControlInventarioContext() : base("name=ControlInventarioContext")
        {
        }

        public System.Data.Entity.DbSet<ControlInventario.Models.Empresa> Empresas { get; set; }

        public System.Data.Entity.DbSet<ControlInventario.Models.Producto> Productos { get; set; }

        public System.Data.Entity.DbSet<ControlInventario.Models.Orden> Ordenes { get; set; }

        public System.Data.Entity.DbSet<ControlInventario.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<ControlInventario.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<ControlInventario.Models.OrdenDetalle> OrdenDetalles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdenDetalle>()
                .HasKey(od => new { od.OrdenDetalleId, od.OrdenId, od.ProductoId });

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Empresas)
                .WithMany(e => e.Clientes);

            //modelBuilder.Entity<Producto>()
            //    .HasMany(p => p.Empresas)
            //    .WithMany(e => e.Productos);
        }
    }
}
