-- Création des tables
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    DateOfBirth DATE NULL,
    GPA DECIMAL(3,2) DEFAULT 0.0 CHECK (GPA BETWEEN 0 AND 4),
    DepartmentId INT NOT NULL
);

CREATE TABLE Professors (
    ProfessorId INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    HireDate DATE NOT NULL,
    Salary DECIMAL(10,2) NOT NULL,
    DepartmentId INT NOT NULL
);

CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) UNIQUE NOT NULL,
    Budget DECIMAL(18,2) NOT NULL
);

CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Credits INT NOT NULL,
    DepartmentId INT NOT NULL
);

CREATE TABLE StudentCourses (
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    Grade DECIMAL(3,2) NULL CHECK (Grade BETWEEN 0 AND 4),
    EnrollmentDate DATE NOT NULL,
    PRIMARY KEY (StudentId, CourseId)
);

-- Insérer des départements (10 exemples)
INSERT INTO Departments (Name, Budget) 
VALUES 
('Computer Science', 500000),
('Mathematics', 300000),
('Physics', 400000),
('Chemistry', 250000),
('Biology', 275000),
('Engineering', 600000),
('Medicine', 700000),
('Economics', 320000),
('History', 150000),
('Philosophy', 180000);

-- Génération massive de données
-- Générer des professeurs (1000 exemples)
DECLARE @Counter INT = 1;
WHILE @Counter <= 1000
BEGIN
    INSERT INTO Professors (LastName, FirstName, HireDate, Salary, DepartmentId)
    VALUES (
        CONCAT('ProfessorLast', @Counter),
        CONCAT('ProfessorFirst', @Counter),
        DATEADD(DAY, -@Counter, GETDATE()),
        ROUND(60000 + (RAND() * 40000), 2),
        FLOOR(RAND() * 10) + 1
    );
    SET @Counter = @Counter + 1;
END;

-- Générer des étudiants (5000 exemples)
SET @Counter = 1;
WHILE @Counter <= 5000
BEGIN
    INSERT INTO Students (LastName, FirstName, Email, DateOfBirth, GPA, DepartmentId)
    VALUES (
        CONCAT('StudentLast', @Counter),
        CONCAT('StudentFirst', @Counter),
        CONCAT('student', @Counter, '@example.com'),
        DATEADD(YEAR, -20, GETDATE()) - @Counter % 365,
        ROUND(RAND() * 4, 2),
        FLOOR(RAND() * 10) + 1
    );
    SET @Counter = @Counter + 1;
END;

-- Générer des cours (200 exemples)
SET @Counter = 1;
WHILE @Counter <= 200
BEGIN
    INSERT INTO Courses (Title, Credits, DepartmentId)
    VALUES (
        CONCAT('CourseTitle', @Counter),
        FLOOR(RAND() * 5) + 1,
        FLOOR(RAND() * 10) + 1
    );
    SET @Counter = @Counter + 1;
END;

-- Générer des inscriptions aux SET @Counter = 1;
WHILE @Counter <= 20000
BEGIN
    DECLARE @StudentId INT = FLOOR(RAND() * 5000) + 1;
    DECLARE @CourseId INT = FLOOR(RAND() * 200) + 1;

    -- Vérifie si la combinaison existe déjà
    IF NOT EXISTS (
        SELECT 1
        FROM StudentCourses
        WHERE StudentId = @StudentId AND CourseId = @CourseId
    )
    BEGIN
        INSERT INTO StudentCourses (StudentId, CourseId, Grade, EnrollmentDate)
        VALUES (
            @StudentId,
            @CourseId,
            ROUND(RAND() * 4, 2),
            DATEADD(DAY, -FLOOR(RAND() * 365), GETDATE())
        );
        SET @Counter = @Counter + 1;
    END;
END;
