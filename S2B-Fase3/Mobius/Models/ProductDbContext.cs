using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class ProductDbContext : DbContext
    {
        // DB Inicial -> Produtos e categorias
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Adição de Comentários (Para produtos...) e Relatorios (Logs...)
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
    }
}