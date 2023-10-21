using Bogus;
using Microsoft.EntityFrameworkCore;
using SimonProj.Models;

namespace SimonProj.Data;

public class Seeder
{
    public static void Run()
    {
        var options = new DbContextOptionsBuilder<ProjContext>()
            .UseSqlServer(
                "Server=localhost;" +
                "Database=Proj;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;" +
                "MultipleActiveResultSets=true"
            ).Options;

        using (var context = new ProjContext(options))
        {
            context.Database.EnsureCreated();

            Seeder seed = new Seeder();
            seed.SeedTeachers(seed, context);
            seed.SeedStudents(seed, context);

            context.SaveChanges();
        }
    }

    private void SeedTeachers(Seeder seed, ProjContext context)
    {
        if (context.Teachers.Any())
        {
            return;
        }

        var teachersList = seed.GenerateFakeTeachers(30);
        context.Teachers.AddRange(teachersList);
    }

    private List<Teacher> GenerateFakeTeachers(int count = 10)
    {
        var faker = new Faker<Teacher>()
            .RuleFor(t => t.FirstName, f => f.Person.FirstName)
            .RuleFor(t => t.LastName, f => f.Person.LastName)
            .RuleFor(t => t.JoinedDate, f => f.Date.Past(5));

        return faker.Generate(count);
    }

    private void SeedStudents(Seeder seed, ProjContext context)
    {
        if (context.Students.Any())
        {
            return;
        }

        var studentList = seed.GenerateFakeStudents(30);
        context.Students.AddRange(studentList);
    }

    private List<Student> GenerateFakeStudents(int count = 10)
    {
        var faker = new Faker<Student>()
            .RuleFor(t => t.FirstName, f => f.Person.FirstName)
            .RuleFor(t => t.LastName, f => f.Person.LastName);

        return faker.Generate(count);
    }
}