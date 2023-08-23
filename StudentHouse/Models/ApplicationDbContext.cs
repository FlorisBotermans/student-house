using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHouse.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<StudentMeal>()
                .HasKey(um => new { um.StudentId, um.MealId });
            modelbuilder.Entity<StudentMeal>()
                .HasOne(um => um.Student)
                .WithMany(u => u.MealsAttend)
                .HasForeignKey(um => um.StudentId);
            modelbuilder.Entity<StudentMeal>()
                .HasOne(um => um.Meal)
                .WithMany(m => m.Guests)
                .HasForeignKey(um => um.MealId);
        }
    }
}
