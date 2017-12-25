using JqTest.Models;
using System;
using System.Data.Entity;

namespace JqTest.DAL
{
    public class JqContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public JqContext()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            Database.SetInitializer(new CreateDatabaseIfNotExists<JqContext>());
        }
    }
}