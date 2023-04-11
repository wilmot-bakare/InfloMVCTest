namespace MyApp.Data.Migrations
{
    using MyApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyApp.Data.MyAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(MyApp.Data.MyAppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.AddOrUpdate(x => x.Id,
       new User() { Id = 1, Forename = "Grant", Surname = "Cooper", Email = "grant.cooper@example.com", DateOfBirth = new DateTime(2000, 11, 07), IsActive = true },
           new User() { Id = 2, Forename = "Tom", Surname = "Gathercole", Email = "tom.gathercole@example.com", DateOfBirth = new DateTime(1990, 12, 24), IsActive = true },
           new User() { Id = 3, Forename = "Mark", Surname = "Edmondson", Email = "mark.edmondson@example.com", DateOfBirth = new DateTime(1976, 05, 13), IsActive = true },
           new User() { Id = 4, Forename = "Graham", Surname = "Clark", Email = "graham.clark@example.com", DateOfBirth = new DateTime(1988, 01, 30), IsActive = true },
           new User() { Id = 5, Forename = "Will", Surname = "Bakare", Email = "will.bakare@example.com", DateOfBirth = new DateTime(1995, 10, 05), IsActive = false },
           new User() { Id = 6, Forename = "Bill", Surname = "Bakare", Email = "Bill.bakare@example.com", DateOfBirth = new DateTime(2012, 09, 22), IsActive = false }
       );
        }
    }
}
