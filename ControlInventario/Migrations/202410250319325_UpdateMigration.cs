namespace ControlInventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductoCategorias", newName: "ProductoCategoria");
            RenameColumn(table: "dbo.ProductoCategoria", name: "Producto_Codigo", newName: "ProductoId");
            RenameColumn(table: "dbo.ProductoCategoria", name: "Categoria_CategoriaId", newName: "CategoriaId");
            RenameIndex(table: "dbo.ProductoCategoria", name: "IX_Producto_Codigo", newName: "IX_ProductoId");
            RenameIndex(table: "dbo.ProductoCategoria", name: "IX_Categoria_CategoriaId", newName: "IX_CategoriaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductoCategoria", name: "IX_CategoriaId", newName: "IX_Categoria_CategoriaId");
            RenameIndex(table: "dbo.ProductoCategoria", name: "IX_ProductoId", newName: "IX_Producto_Codigo");
            RenameColumn(table: "dbo.ProductoCategoria", name: "CategoriaId", newName: "Categoria_CategoriaId");
            RenameColumn(table: "dbo.ProductoCategoria", name: "ProductoId", newName: "Producto_Codigo");
            RenameTable(name: "dbo.ProductoCategoria", newName: "ProductoCategorias");
        }
    }
}
