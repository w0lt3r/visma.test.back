using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Infrastructure.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
        //public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(new object[] {
                new Employee{Id=1, FirstName="Hugo",LastName="Romero",SocialSecurityNumber="123456",Phone="996213141",Active=true},
                new Employee{Id=2, FirstName="Alexa",LastName="Perez",SocialSecurityNumber="789542",Phone=null,Active=true},
                new Employee{Id=3, FirstName="Maria",LastName="Quispe",SocialSecurityNumber="653241",Phone=null,Active=true},
                new Employee{Id=4, FirstName="Bryan",LastName="Cranston",SocialSecurityNumber="687541",Phone="981123123",Active=true} });
            // modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
