using correction_tp_final.Context;
using correction_tp_final.Services;
using Microsoft.EntityFrameworkCore;

namespace TestFinalTP;

[TestClass]
public class AppTest
{
    private AppDbContext _context;
    private StudentService _studentService;

    [TestInitialize]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(
                "Server=localhost,1433;Database=tpfinal;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True;")
            .Options;

        _context = new AppDbContext(options);

        _studentService = new StudentService(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public async Task GetHighPerformingStudentsAsync_ShouldReturnCorrectResults()
    {
        var result = await _studentService.GetHighPerformingStudentsAsync();

        Assert.IsTrue(result.Any());
        Assert.IsTrue(result.All(s => s.Gpa > (decimal?)3.5));
    }

    [TestMethod]
    public async Task GetHighPerformingStudentsWithProjectionAsync_ShouldReturnCorrectDTOs()
    {
        var result = await _studentService.GetHighPerformingStudentsWithProjectionAsync();

        Assert.IsTrue(result.Any());
        Assert.IsTrue(result.All(s => s.GPA > (decimal)3.5));
    }

    [TestMethod]
    public async Task GetDepartmentsWithAverageSalariesAsync_ShouldReturnDepartmentsWithCorrectSalaries()
    {
        var result = await _studentService.GetDepartmentsWithAverageSalariesAsync();

        Assert.IsTrue(result.Any());
        Assert.IsTrue(result.All(d => d.AverageSalary >= 0));
    }

    [TestMethod]
    public async Task SearchCoursesAsync_WithFilters_ShouldReturnFilteredResults()
    {
        var result = await _studentService.SearchCoursesAsync(
            minEnrollments: 5,
            keyword: "CourseTitle20",
            minCredits: 3,
            maxCredits: 5
        );

        Assert.IsTrue(result.Any());
        Assert.IsTrue(result.All(c => c.CourseTitle.Contains("CourseTitle20")));
        Assert.IsTrue(result.All(c => c.Credits >= 3 && c.Credits <= 5));
    }

    [TestMethod]
    public async Task SearchCoursesAsync_WithoutFilters_ShouldReturnAllCourses()
    {
        var result = await _studentService.SearchCoursesAsync(
            minEnrollments: null,
            keyword: null,
            minCredits: null,
            maxCredits: null
        );

        var allCourses = await _context.Courses.CountAsync();

        Assert.AreEqual(allCourses, result.Count);
    }
    
    
    [TestMethod]
    public async Task PerformTransactionAsync_ShouldCompleteSuccessfully()
    {
        var studentId = 1;
        var courseIds = new[] { 2, 3 };

        var result = await _studentService.PerformTransactionAsync(studentId, courseIds);

        Assert.IsTrue(result);

        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        Assert.IsNotNull(student);
        Assert.IsTrue(student.StudentCourses.Count >= 2);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task PerformTransactionAsync_ShouldRollbackOnFailure()
    {
        var studentId = 1;
        var courseIds = new[] { 999 }; // Nonexistent course ID

        await _studentService.PerformTransactionAsync(studentId, courseIds);

        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        Assert.IsTrue(student.StudentCourses.Count == 0);
    }
}