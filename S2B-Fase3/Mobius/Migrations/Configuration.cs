namespace Mobius.Migrations
{
    using Microsoft.AspNet.Identity;
    using Mobius.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(
                u => u.UserName,
                new ApplicationUser
                {
                    Id = "ADM01",
                    Email = "admin@s2b.br",
                    UserName = "admin@s2b.br",
                    PasswordHash = hasher.HashPassword("s2b"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = "USR01",
                    Email = "hugo@s2b.br",
                    UserName = "hugo@s2b.br",
                    PasswordHash = hasher.HashPassword("s2b"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = "USR10",
                    Email = "ze@s2b.br",
                    UserName = "ze@s2b.br",
                    PasswordHash = hasher.HashPassword("s2b"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = "USR11",
                    Email = "luis@s2b.br",
                    UserName = "luis@s2b.br",
                    PasswordHash = hasher.HashPassword("s2b"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );
        }
    }
}
