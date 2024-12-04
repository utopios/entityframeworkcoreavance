using BenchmarkDotNet.Attributes;
using correction_tp_final.Context;
using correction_tp_final.Entities;
using Microsoft.EntityFrameworkCore;

namespace correction_tp_final.Benchmark;

[MemoryDiagnoser]
public class Benchmarking
{
    private AppDbContext _context;

    [GlobalSetup]
    public async Task Setup()
    {
        _context = new AppDbContext();
    }

    [Benchmark]
    public List<Student> LazyLoading()
    {
        var students = _context.Students;
        foreach (var student in students)
        {
            var courses = student.StudentCourses.ToList();
            Console.WriteLine(courses);
        }
        return students.ToList();
    }

    [Benchmark]
    public List<Student> EagerLoading()
    {
        return _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .ToList();
    }

    [Benchmark]
    public List<Student> ExplicitLoading()
    {
        var students = _context.Students;
        foreach (var student in students)
        {
            _context.Entry(student)
                .Collection(s => s.StudentCourses)
                .Query()
                .Include(sc => sc.Course)
                .Load();
        }
        return students.ToList();
    }
}