using correction_tp_final.Context;
using correction_tp_final.Entities;
using Microsoft.EntityFrameworkCore;

namespace correction_tp_final.Services;

public class StudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Student>> GetHighPerformingStudentsAsync()
    {
        return await _context.Students
            .Where(s => s.Gpa > (decimal?)3.5)
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .ThenInclude(c => c.Department)
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .ThenInclude(c => c.Department.Professors)
            .ToListAsync();
    }

    public async Task<List<StudentDTO>> GetHighPerformingStudentsWithProjectionAsync()
    {
        return await _context.Students
            .Where(s => s.Gpa > (decimal?)3.5)
            .Select(s => new StudentDTO(
                $"{s.FirstName} {s.LastName}",
                (decimal)s.Gpa,
                s.StudentCourses.Select(sc => new CourseDTO(
                    sc.Course.Title,
                    sc.Course.Credits,
                    sc.Course.Department.Name,
                    sc.Course.Department.Professors.Select(p => $"{p.FirstName} {p.LastName}").ToList()
                )).ToList()
            ))
            .ToListAsync();
    }

    public async Task<List<DepartmentDTO>> GetDepartmentsWithAverageSalariesAsync()
    {
        return await _context.Departments
            .Select(d => new DepartmentDTO(d.Name, d.Budget,
                AppDbContext.GetAverageProfessorSalary(d.DepartmentId) ?? 0
            ))
            .ToListAsync();
    }

    public async Task<List<CourseEnrollmentsDTO>> SearchCoursesAsync(
        int? minEnrollments,
        string keyword,
        int? minCredits,
        int? maxCredits)
    {
        var query = _context.Courses.AsQueryable();

        if (minEnrollments.HasValue)
        {
            query = query.Where(c => c.StudentCourses.Count >= minEnrollments.Value);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(c => EF.Functions.Like(c.Title, $"%{keyword}%"));
        }

        if (minCredits.HasValue && maxCredits.HasValue)
        {
            query = query.Where(c => c.Credits >= minCredits.Value && c.Credits <= maxCredits.Value);
        }

        return await query.Select(c => new CourseEnrollmentsDTO(
            c.Title,
            c.Credits,
            c.StudentCourses.Count
        )).ToListAsync();
    }
    
    public async Task<List<CourseFullTextDTO>> SearchCoursesByKeywordAsync(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
        {
            return new List<CourseFullTextDTO>();
        }

        return await _context.Courses
            .Where(c => EF.Functions.FreeText(c.Title, keyword))
            .Select(c => new CourseFullTextDTO(
                c.Title,
                c.Credits
            ))
            .OrderBy(c => c.CourseTitle)
            .ToListAsync();
    }
    
    public async Task<bool> PerformTransactionAsync(int studentId, int[] courseIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Name == "Computer Science");

            if (department == null)
                throw new Exception("Department not found");

            department.Budget *= 1.1m;

            var student = await _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student == null)
                throw new Exception("Student not found");

            foreach (var courseId in courseIds)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
                if (course == null)
                    throw new Exception($"Course with ID {courseId} not found");

                student.StudentCourses.Add(new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}



public record CourseFullTextDTO(
    string CourseTitle,
    int Credits
);

public record CourseEnrollmentsDTO(
    string CourseTitle,
    int Credits,
    int EnrollmentsCount);

public record DepartmentDTO(
    string DepartmentName,
    decimal Budget,
    decimal AverageSalary
);

public record StudentDTO(
    string StudentName,
    decimal GPA,
    List<CourseDTO> Courses
);

public record CourseDTO(
    string CourseTitle,
    int Credits,
    string DepartmentName,
    List<string> ResponsibleProfessors
);