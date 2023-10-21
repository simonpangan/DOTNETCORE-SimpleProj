using Microsoft.EntityFrameworkCore;
using SimonProj.Models;

namespace SimonProj.Data;

public class ProjContext : DbContext
{
    public ProjContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>().ToTable("Teacher");
        modelBuilder.Entity<Student>().ToTable("Student");

        // Configure the one-to-many relationship between Teacher and Student.
        modelBuilder.Entity<Teacher>()
            .HasMany(teacher => teacher.Students)  // Teacher has many Students
            .WithOne(student => student.Teacher)    // Student belongs to one Teacher
            .HasForeignKey(student => student.TeacherId)  // Define the foreign key

            // Set the delete behavior to restrict. This means that when you attempt to
            // delete a Teacher, it will be restricted if there are associated Students.
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the many-to-one relationship between Student and Teacher.
        modelBuilder.Entity<Student>()
            .HasOne(student => student.Teacher)    // Student has one Teacher
            .WithMany(teacher => teacher.Students)  // Teacher can have many Students

            // Set the delete behavior to restrict. This means that when you attempt to
            // delete a Student, it will be restricted if there is an associated Teacher.
            .OnDelete(DeleteBehavior.Restrict);

    }
}