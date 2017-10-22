using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using Basico.Entities;

namespace Basico.Data
{
    public class BasicoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BasicoContext>
    {
        protected override void Seed(BasicoContext context)
        {
            context.set_identityRole.AddOrUpdate(r => r.Name, GenerateRoles());
            context.set_identityUser.AddOrUpdate(u => u.Email, new identityUser[]{
                new identityUser()
                {
                    Email="root@root.com",
                    Username="root",
                    Firstname="marcelo",
                    HashedPassword ="79RF4FDP2U+e7Na84Ii8UuUQYmXVv2SPD9JQxXBrtnU=",
                    Salt = "8O4nsKmO/+KxHVyddRPyIw==",
                    IsLocked = false,
                    DateCreated = DateTime.UtcNow
                }
            });

            context.set_identityUserRole.AddOrUpdate(new identityUserRole[] {
                    new identityUserRole() {
                        identityRoleID = 1, // root
                        identityUserID = 1  // chsakell
                    }
                });
        }
        private identityRole[] GenerateRoles()
        {
            identityRole[] _identityRoles = new identityRole[]{
                new identityRole()
                {
                    Name="Root"
                },
                new identityRole()
                {
                    Name="Admin"
                },
                new identityRole()
                {
                    Name="Usuário"
                },
                new identityRole()
                {
                    Name="Convidado"
                }
            };

            return _identityRoles;
        }
    }
}

