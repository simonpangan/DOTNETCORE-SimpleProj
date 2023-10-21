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
        context.SaveChanges(); // Save the teachers to generate TeacherId values

        var studentList = seed.GenerateFakeStudents(50);
        foreach (var student in studentList)
        {
            var randomTeacher = teachersList[new Random().Next(teachersList.Count)];
            student.Teacher = randomTeacher;
        }

        context.Students.AddRange(studentList);
        context.SaveChanges();
    }

    private List<Teacher> GenerateFakeTeachers(int count = 10)
    {
        var faker = new Faker<Teacher>()
            .RuleFor(t => t.FirstName, f => f.Person.FirstName)
            .RuleFor(t => t.LastName, f => f.Person.LastName)
            .RuleFor(t => t.JoinedDate, f => f.Date.Past(5));

        return faker.Generate(count);
    }

    private List<Student> GenerateFakeStudents(int count = 10)
    {
        var faker = new Faker<Student>()
            .RuleFor(t => t.FirstName, f => f.Person.FirstName)
            .RuleFor(t => t.LastName, f => f.Person.LastName);

        return faker.Generate(count);
    }
}