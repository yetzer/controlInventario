namespace ControlInventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Producto_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.CategoriaId)
                .ForeignKey("dbo.Productos", t => t.Producto_Codigo)
                .Index(t => t.Producto_Codigo);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Correo = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        NIT = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.NIT);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Caracteristicas = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmpresaNIT = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Empresas", t => t.EmpresaNIT)
                .Index(t => t.EmpresaNIT);
            
            CreateTable(
                "dbo.OrdenDetalles",
                c => new
                    {
                        OrdenDetalleId = c.Int(nullable: false),
                        OrdenId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidadproducto = c.Int(nullable: false),
                        ValorProducto = c.Int(nullable: false),
                        Producto_Codigo = c.Int(),
                    })
                .PrimaryKey(t => new { t.OrdenDetalleId, t.OrdenId, t.ProductoId })
                .ForeignKey("dbo.Ordens", t => t.OrdenId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.Producto_Codigo)
                .Index(t => t.OrdenId)
                .Index(t => t.Producto_Codigo);
            
            CreateTable(
                "dbo.Ordens",
                c => new
                    {
                        OrdenId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdenId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.ClienteEmpresas",
                c => new
                    {
                        Cliente_ClienteId = c.Int(nullable: false),
                        Empresa_NIT = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Cliente_ClienteId, t.Empresa_NIT })
                .ForeignKey("dbo.Clientes", t => t.Cliente_ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Empresas", t => t.Empresa_NIT, cascadeDelete: true)
                .Index(t => t.Cliente_ClienteId)
                .Index(t => t.Empresa_NIT);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClienteEmpresas", "Empresa_NIT", "dbo.Empresas");
            DropForeignKey("dbo.ClienteEmpresas", "Cliente_ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.OrdenDetalles", "Producto_Codigo", "dbo.Productos");
            DropForeignKey("dbo.OrdenDetalles", "OrdenId", "dbo.Ordens");
            DropForeignKey("dbo.Ordens", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Productos", "EmpresaNIT", "dbo.Empresas");
            DropForeignKey("dbo.Categorias", "Producto_Codigo", "dbo.Productos");
            DropIndex("dbo.ClienteEmpresas", new[] { "Empresa_NIT" });
            DropIndex("dbo.ClienteEmpresas", new[] { "Cliente_ClienteId" });
            DropIndex("dbo.Ordens", new[] { "ClienteId" });
            DropIndex("dbo.OrdenDetalles", new[] { "Producto_Codigo" });
            DropIndex("dbo.OrdenDetalles", new[] { "OrdenId" });
            DropIndex("dbo.Productos", new[] { "EmpresaNIT" });
            DropIndex("dbo.Categorias", new[] { "Producto_Codigo" });
            DropTable("dbo.ClienteEmpresas");
            DropTable("dbo.Ordens");
            DropTable("dbo.OrdenDetalles");
            DropTable("dbo.Productos");
            DropTable("dbo.Empresas");
            DropTable("dbo.Clientes");
            DropTable("dbo.Categorias");
        }
    }
}
