using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;

namespace FunctionAppV1
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
        [FunctionName("FunctionV1")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
        HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("Running V1...");

            // **** THIS EXECUTES SUCCESSFULLY IN V1
            using (var connection =
                new Microsoft.AnalysisServices.AdomdClient.AdomdConnection("Provider=MSOLAP;Data Source=localhost"))
            {
                // only including the Open call to show exception; otherwise, it requires access to a SSAS server
                connection.Open();
            }

            // **** THIS FAILS IN V1
            // System.IO.FileNotFoundException: 
            //'Could not load file or assembly 'System.ComponentModel.Annotations, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. The system cannot find the file specified.'
            using (var context = new MyDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var data = string.Join(", ", context.People.Select(p => p.Name));
            }

            return req.CreateResponse(HttpStatusCode.OK, "Completed!");
        }
    }
}
