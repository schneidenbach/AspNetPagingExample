using AspNetPagingExample.Models.Entities;

namespace AspNetPagingExample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AspNetPagingExample.Models.Entities.EntityContext";
        }

        protected override void Seed(EntityContext context)
        {
            var employees = Enumerable.Range(1, 100).Select(i => new Employee
            {
                FirstName = "First" + i,
                LastName = "Last" + i
            }).ToArray();

            context.Employees.AddOrUpdate(e => e.FirstName, employees);
            context.Todos.AddOrUpdate(t => t.Name, Enumerable.Range(1, 100).SelectMany(i => Enumerable.Range(1, 5).Select(j => new Todo
            {
                Employee = employees.First(e => e.FirstName == "First" + i),
                Name = $"Task #{j} for Employee {i}",
                Description = $"Description for Task #{j} for Employee {i}"
            })).ToArray());
        }
    }
}
