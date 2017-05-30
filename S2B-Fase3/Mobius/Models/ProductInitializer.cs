using Mobius.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

//TODO Initialize Products/Users

namespace MvcMovie.Models
{
     public class ProductInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProductDbContext>
    {
        protected override void Seed(ProductDbContext context)
        {
             var categories = new List<Category>
             {
                new Category { Name = "Roupas", Description = "Vestimentas de todos os tipos." },
                new Category { Name = "Acessórios", Description = "Acessórios pessoais." },
                new Category { Name = "Instrumentos", Description = "Instrumentos musicais variados." },
                new Category { Name = "Eletrodomésticos", Description = "Todos os tipos de Eletrodomésticos." },
                new Category { Name = "Utensílios", Description = "Utensílos para qualquer atividade." },
                new Category { Name = "Construção", Description = "Ferramentas e materiais." },
                new Category { Name = "Móveis", Description = "Móveis internos ou externos." },
                new Category { Name = "Esportes", Description = "Artigos esportivos, como vestimentas, acessórios ou veículos." },
                new Category { Name = "Livros", Description = "Livros de vários gêneros." },
                new Category { Name = "Decoração", Description = "Decoração para o lar." },
                new Category { Name = "Animais e Acessórios", Description = "Tudo para seu animal de estimação." },
                new Category { Name = "Acessórios Infantis", Description = "Acessórios variados para sua criança." },
                new Category { Name = "Roupas Infantis", Description = "Roupas para sua criança." },
                new Category { Name = "Brinquedos", Description = "Brinquedos variados." },
             };


            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            //var movies = new List<Product>
            //{
            //    new Product
            //    {
            //        Title = "Camisa Azul",
            //        Description = "Camisa da cor azul, feita por Empresa Y, de tamanho M.",
            //        Cost = 15.00m,
            //        Address = "",
            //        PublishDate = DateTime.Parse("2017-1-13"),
            //        ExpirationDate = DateTime.Parse("2017-3-14"),
            //        Status = Status.Open,
            //        CategoryID = categories.Single(c => c.Name == "Roupas").CategoryID,
            //        //UserID = 
            //    },
            //    new Product
            //    {
            //        Title = "Calça Jeans",
            //        Description = "Calça jeans, pouco usada, tamanho 44, clara.",
            //        Cost = 35.00m,
            //        Address = "",
            //        PublishDate = DateTime.Parse("2017-1-13"),
            //        ExpirationDate = DateTime.Parse("2017-3-14"),
            //        Status = Status.Open,
            //        CategoryID = categories.Single(c => c.Name == "Roupas").CategoryID,
            //        //UserID = 
            //    },

            //    new Product{Title = "Pingente dourado", Description = "Pingente dourado", Cost = 25.00m, Address = "", PublishDate = DateTime.Parse("2017-3-14"), ExpirationDate = DateTime.Parse("2017-5-13"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Acessórios").CategoryID, /*UserID = */},
            //    new Product{Title = "Anel Prateado", Description = "Anel Prateado", Cost = 15.00m, Address = "", PublishDate = DateTime.Parse("2017-3-14"), ExpirationDate = DateTime.Parse("2017-5-13"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Acessórios").CategoryID, /*UserID = */},

            //    new Product{Title = "Violão", Description = "Violão pouco usado, necessário trocar as cordas.", Cost = 100.00m, Address = "", PublishDate = DateTime.Parse("2017-2-11"), ExpirationDate = DateTime.Parse("2017-4-12"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Instrumentos").CategoryID, /*UserID = */},
            //    new Product{Title = "Kantele Artesanal", Description = "Instrumento típico da finlância, com 5 cordas. Feito à mão.", Cost = 300.00m, Address = "", PublishDate = DateTime.Parse("2017-2-11"), ExpirationDate = DateTime.Parse("2017-4-12"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Instrumentos").CategoryID, /*UserID = */},

            //    new Product{Title = "Liquidificador Electrolux", Description = "Liquidificador pouco usado, ainda funciona.", Cost = 99.00m, Address = "", PublishDate = DateTime.Parse("2017-1-1"), ExpirationDate = DateTime.Parse("2017-3-2"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Eletrodomésticos").CategoryID, /*UserID = */},
            //    new Product{Title = "Máquina de lavar usada", Description = "Máquina de lavar usada.", Cost = 499.99m, Address = "", PublishDate = DateTime.Parse("2017-1-1"), ExpirationDate = DateTime.Parse("2017-3-2"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Eletrodomésticos").CategoryID, /*UserID = */},

            //    new Product{Title = "Conjuntos de talheres INOX", Description = "Conjunto de talheres em aço inox, 42 peças.", Cost = 110.00m, Address = "", PublishDate = DateTime.Parse("2017-3-2"), ExpirationDate = DateTime.Parse("2017-5-1"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Utensílios").CategoryID, /*UserID = */},
            //    new Product{Title = "Ralador Inox 4 faces 17cm", Description = "Ralador com 4 faces produzido em aço inox.", Cost = 28.00m, Address = "", PublishDate = DateTime.Parse("2017-3-2"), ExpirationDate = DateTime.Parse("2017-5-1"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Utensílios").CategoryID, /*UserID = */},

            //    new Product{Title = "Cortador de grama", Description = "Cortador de grama Tramontina, pouco usado.", Cost = 69.99m, Address = "", PublishDate = DateTime.Parse("2017-2-15"), ExpirationDate = DateTime.Parse("2017-4-16"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Construção").CategoryID, /*UserID = */},
            //    new Product{Title = "Parafusadeira", Description = "Parafusadeira elétrica, pouco usada. Bivolt.", Cost = 150.00m, Address = "", PublishDate = DateTime.Parse("2017-2-15"), ExpirationDate = DateTime.Parse("2017-4-16"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Construção").CategoryID, /*UserID = */},

            //    new Product{Title = "Cadeira de escritório", Description = "Cadeira de escritório branca.", Cost = 50.00m, Address = "", PublishDate = DateTime.Parse("2017-2-1"), ExpirationDate = DateTime.Parse("2017-4-2"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Móveis").CategoryID, /*UserID = */},
            //    new Product{Title = "Sofá-Cama", Description = "Sofá-Cama com 3 lugares.", Cost = 300.00m, Address = "", PublishDate = DateTime.Parse("2017-2-1"), ExpirationDate = DateTime.Parse("2017-4-2"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Móveis").CategoryID, /*UserID = */},

            //    new Product{Title = "Bicicleta", Description = "Vendo bicicleta de alumínio em bom estado.", Cost = 220.00m, Address = "", PublishDate = DateTime.Parse("2017-4-1"), ExpirationDate = DateTime.Parse("2017-5-31"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Esportes").CategoryID, /*UserID = */},
            //    new Product{Title = "Halteres 2kg", Description = "Par de halteres de 2kgs.", Cost = 20.00m, Address = "", PublishDate = DateTime.Parse("2017-4-1"), ExpirationDate = DateTime.Parse("2017-5-31"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Esportes").CategoryID, /*UserID = */},

            //    new Product{Title = "Coleção Paulo Coelho", Description = "Coleção de livros do autor Paulo Coelho.", Cost = 100.00m, Address = "", PublishDate = DateTime.Parse("2017-4-2"), ExpirationDate = DateTime.Parse("2017-7-1"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Livros").CategoryID, /*UserID = */},
            //    new Product{Title = "O código da Vinci", Description = "Livro O Código da Vinci, de Dan Brown", Cost = 15.00m, Address = "", PublishDate = DateTime.Parse("2017-4-2"), ExpirationDate = DateTime.Parse("2017-7-1"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Livros").CategoryID, /*UserID = */},

            //    new Product{Title = "Vaso de Porcelana", Description = "Vaso antigo de porcelana esmaltada, com relevo, 21cm de altura.", Cost = 30.00m, Address = "", PublishDate = DateTime.Parse("2017-4-2"), ExpirationDate = DateTime.Parse("2017-7-2"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Decoração").CategoryID, /*UserID = */},
            //    new Product{Title = "Tapete Bege", Description = "Tapete bege 2,50x200 , sem manchas.", Cost = 120.00m, Address = "", PublishDate = DateTime.Parse("2017-4-2"), ExpirationDate = DateTime.Parse("2017-7-2"), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Decoração").CategoryID, /*UserID = */},

            //    new Product{Title = "Roupa para Cachorro", Description = "Roupa para cachorro de porte pequeno. Pouco usada.", Cost = 20.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Animais e Acessórios").CategoryID, /*UserID = */},
            //    new Product{Title = "Casinha de cachorro", Description = "Casinha de cachorro personalizada.", Cost = 60.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Animais e Acessórios").CategoryID, /*UserID = */},

            //    new Product{Title = "", Description = "", Cost = 0.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Acessórios Infantis").CategoryID, /*UserID = */},
            //    new Product{Title = "", Description = "", Cost = 0.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Acessórios Infantis").CategoryID, /*UserID = */},

            //    new Product{Title = "", Description = "", Cost = 0.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Roupas Infantis").CategoryID, /*UserID = */},
            //    new Product{Title = "", Description = "", Cost = 0.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Roupas Infantis").CategoryID, /*UserID = */},

            //    new Product{Title = "", Description = "", Cost = 0.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Brinquedos").CategoryID, /*UserID = */},
            //    new Product{Title = "", Description = "", Cost = 0.00m, Address = "", PublishDate = DateTime.Parse(""), ExpirationDate = DateTime.Parse(""), Status = Status.Open, CategoryID = categories.Single(c => c.Name == "Brinquedos").CategoryID, /*UserID = */}
            //};

            //movies.ForEach(s => context.Products.Add(s));
            //context.SaveChanges();
        }
    }
}