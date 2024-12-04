use tpfinal;
CREATE FUNCTION GetAverageProfessorSalary(@DepartmentId INT)
    RETURNS DECIMAL(10,2)
AS
BEGIN
RETURN (
    SELECT AVG(Salary)
    FROM Professors
    WHERE DepartmentId = @DepartmentId
);
END;


CREATE UNIQUE INDEX PK_Courses ON Courses(CourseId);

CREATE FULLTEXT CATALOG CoursesFTCatalog AS DEFAULT;

CREATE FULLTEXT INDEX ON Courses (Title LANGUAGE 1033)
    KEY INDEX PK_Courses;
