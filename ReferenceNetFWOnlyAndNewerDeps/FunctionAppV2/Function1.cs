using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;

namespace FunctionAppV2
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "AAAA" },
                new Person { Id = 2, Name = "BBBB" },
                new Person { Id = 3, Name = "CCCC" }
            );
        }
    }

    public static class Function1
    {
        [FunctionName("FunctionV2")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req,
            TraceWriter log)
        {
            log.Info("Running V2...");

            // **** THIS EXECUTES SUCCESSFULLY IN V2
            using (var context = new MyDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var data = string.Join(", ", context.People.Select(p => p.Name));
            }

            // **** THIS FAILS IN V2
            // System.Private.CoreLib: Exception while executing function: FunctionV2. 
            // Microsoft.AnalysisServices.AdomdClient: Could not load type 'System.Security.Principal.WindowsImpersonationContext' from assembly 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
            using (var connection =
                new Microsoft.AnalysisServices.AdomdClient.AdomdConnection("Provider=MSOLAP;Data Source=localhost"))
            {
                // only including the Open call to show exception; otherwise, it requires access to a SSAS server
                connection.Open();
                // ... more code
            }

            return (ActionResult)new OkObjectResult("Completed!");
        }
    }
}
