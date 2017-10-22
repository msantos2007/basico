namespace Basico.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Basico.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Basico.Data.BasicoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Basico.Data.BasicoContext context)
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


            //Marcelo - Comentado para testes de migração
            //context.set_identityRole.AddOrUpdate(r => r.Name, GenerateRoles());
            //context.set_identityUser.AddOrUpdate(u => u.Email, new identityUser[]{
            //    new identityUser()
            //    {
            //        Email="root@root.com",
            //        Username="root",
            //        Firstname="marcelo",
            //        HashedPassword ="79RF4FDP2U+e7Na84Ii8UuUQYmXVv2SPD9JQxXBrtnU=",
            //        Salt = "8O4nsKmO/+KxHVyddRPyIw==",
            //        IsLocked = false,
            //        DateCreated = DateTime.UtcNow
            //    }
            //});

            //context.set_identityUserRole.AddOrUpdate(new identityUserRole[] {
            //        new identityUserRole() {
            //            identityRoleID = 1, // root
            //            identityUserID = 1  // chsakell
            //        }
            //    });
        }
        //private identityRole[] GenerateRoles()
        //{
        //    identityRole[] _identityRoles = new identityRole[]{
        //        new identityRole()
        //        {
        //            Name="Root"
        //        },
        //        new identityRole()
        //        {
        //            Name="Admin"
        //        },
        //        new identityRole()
        //        {
        //            Name="Usuário"
        //        },
        //        new identityRole()
        //        {
        //            Name="Convidado"
        //        }
        //    };

        //    return _identityRoles;
        //}

    }
}
