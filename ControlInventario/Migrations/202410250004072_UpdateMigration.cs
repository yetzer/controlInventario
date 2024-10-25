namespace ControlInventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categorias", "Producto_Codigo", "dbo.Productos");
            DropIndex("dbo.Categorias", new[] { "Producto_Codigo" });
            CreateTable(
                "dbo.ProductoCategorias",
                c => new
                    {
                        Producto_Codigo = c.Int(nullable: false),
                        Categoria_CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Producto_Codigo, t.Categoria_CategoriaId })
                .ForeignKey("dbo.Productos", t => t.Producto_Codigo, cascadeDelete: true)
                .ForeignKey("dbo.Categorias", t => t.Categoria_CategoriaId, cascadeDelete: true)
                .Index(t => t.Producto_Codigo)
                .Index(t => t.Categoria_CategoriaId);
            
            DropColumn("dbo.Categorias", "Producto_Codigo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categorias", "Producto_Codigo", c => c.Int());
            DropForeignKey("dbo.ProductoCategorias", "Categoria_CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.ProductoCategorias", "Producto_Codigo", "dbo.Productos");
            DropIndex("dbo.ProductoCategorias", new[] { "Categoria_CategoriaId" });
            DropIndex("dbo.ProductoCategorias", new[] { "Producto_Codigo" });
            DropTable("dbo.ProductoCategorias");
            CreateIndex("dbo.Categorias", "Producto_Codigo");
            AddForeignKey("dbo.Categorias", "Producto_Codigo", "dbo.Productos", "Codigo");
        }
    }
}
