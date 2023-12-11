USE Master
GO

CREATE DATABASE K3_Lab1_DB
GO

USE K3_Lab1_DB
GO

CREATE TABLE Classes (
	ClassID INT PRIMARY KEY Identity(1,1),
	ClassName NVARCHAR(35) NOT NULL
)
GO


CREATE TABLE Students (
	StudentID INT PRIMARY KEY Identity(1,1),
	FirstName NVARCHAR(35) NOT NULL,
	LastName NVARCHAr(35) NOT NULL,
	SSN INT UNIQUE,
	DateOfBirth DATE NOT NULL,
	Gender CHAR,
	ClassID_FK INT NOT NULL,
	FOREIGN KEY (ClassID_FK) REFERENCES Classes(ClassID)
)
GO

CREATE TABLE Personal (
	PersonalID INT PRIMARY KEY Identity(1,1),
	FirstName NVARCHAR(35) NOT NULL,
	LastName NVARCHAr(35) NOT NULL,
	PersonalCategory NVARCHAR(35)
)
GO

CREATE TABLE Courses (
	CourseID INT PRIMARY KEY Identity(1,1),
	CourseName NVARCHAR(35) NOT NULL,
	PersonalID_FK INT, 
	FOREIGN KEY (PersonalID_FK) REFERENCES Personal(PersonalID)
)
GO

CREATE TABLE Enrollments(
	EnrollmentID INT PRIMARY KEY Identity(1,1),
	StudentID_FK INT NOT NULL,
	CourseID_FK INT NOT NULL,
	Grade INT,
	GradeSetInDate Date,
	FOREIGN KEY (StudentID_FK) REFERENCES Students(StudentID),
	FOREIGN KEY (CourseID_FK) REFERENCES Courses(CourseID)
)
GO

INSERT INTO Classes (ClassName)
Values
('NET23'), ('JS23'), ('UX23')
GO

INSERT INTO Students (FirstName, LastName, SSN, DateOfBirth, Gender, ClassID_FK)
Values
('Alex', 'Johansson', 100100, '2000-01-01', 'M', 1),
('Brida', 'Eriksson', 100200, '2001-05-01', 'F', 1),
('Hugo', 'Nilsson', 103320, '2000-12-12', 'M', 1),
('Alice', 'Lindberg', 200123, '1999-04-03', 'M', 2),
('Leo', 'Karlsson', 300200, '2001-11-01', 'M', 2),
('Maja', 'Andersson', 131260, '2000-10-23', 'M', 2),
('Vera', 'Svensson', 400200, '2000-06-01', 'F', 3),
('Oscar', 'Gustafsson', 500300, '1999-12-31', 'M', 3),
('Noah', 'Eriksson', 600200, '2000-07-01', 'M', 3)
GO

INSERT INTO Personal(FirstName, LastName, PersonalCategory)
Values
('Emil', 'Pettersson', 'Teacher'), 
('Alma', 'Larsson', 'Teacher'),
('William', 'Nilsson', 'Substitute'),
('Elsa', 'Johansson', 'Adminstrator'), 
('Oliver', 'Berg', 'Principal')
GO

INSERT INTO Courses(CourseName, PersonalID_FK)
Values
('IT Tech & Operations', 1),
('C#', 2),
('JavaScript', 2),
('Webdevelopment', 3)
GO

INSERT INTO Enrollments (StudentID_FK, CourseID_FK, Grade, GradeSetInDate)
Values
(2, 1, 80, '2023-10-05'),      --I don't know why the StudentID have to start with 2, I set the Identity(1,1) above, maybe I deleted 1 record before :(
(2, 2, 75, '2023-11-11'),
(3, 1, 90, '2023-10-10'), 
(3, 3, 70, '2023-11-23'),
(4, 1, 85, '2023-10-01'),
(4, 4, 60, '2023-11-15'),
(5, 1, 65, '2023-10-05'),
(5, 2, 70, '2023-11-11'),
(6, 1, 80, '2023-10-10'), 
(6, 3, 75, '2023-11-23'),
(7, 1, 85, '2023-10-01'),
(7, 4, 75, '2023-11-15'),
(8, 1, 75, '2023-10-05'),
(8, 2, 70, '2023-11-11'),
(9, 1, 95, '2023-10-10'), 
(9, 3, 75, '2023-11-23'),
(10, 1, 90, '2023-10-01'),
(10, 4, 65, '2023-11-15')
GO




