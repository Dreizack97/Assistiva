CREATE DATABASE Assistiva
GO

USE Assistiva
GO

CREATE TABLE Roles(
    RoleId INT IDENTITY(1, 1) CONSTRAINT PK_Roles PRIMARY KEY NOT NULL,
    [Name] NVARCHAR(25) CONSTRAINT UQ_Roles_Name UNIQUE NOT NULL -- No permite datos duplicados
)
GO

INSERT INTO Roles([Name]) VALUES ('Administrador'), ('Director'), ('Docente'), ('Auxiliar'), ('Estudiante')
GO

CREATE TABLE Users(
    UserId INT IDENTITY(1, 1) CONSTRAINT PK_Users PRIMARY KEY NOT NULL,
    RoleId INT NOT NULL,
    Username NVARCHAR(50) CONSTRAINT UQ_Users_Username UNIQUE NOT NULL, -- No permite datos duplicados
    Salt VARBINARY(32) NOT NULL,
    [Password] VARBINARY(32) NOT NULL,
    Email NVARCHAR(100) CONSTRAINT UQ_Users_Email UNIQUE NOT NULL, -- No permite datos duplicados
    UrlPicture NVARCHAR(200) NULL,
    RecoveryCode NVARCHAR(16) NULL,
    ExpirationCode DATETIME NULL,
    IsPasswordReset BIT NULL DEFAULT 0,
    LastPasswordReset DATETIME NULL,
    IsPasswordDefect BIT DEFAULT 1 NOT NULL,
    LastPasswordChange DATETIME DEFAULT GETDATE() NOT NULL,
	CreatedAt DATETIME DEFAULT GETDATE() NOT NULL,
	UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1 NOT NULL,
    CONSTRAINT FK_Users_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
)
GO

DECLARE @Salt VARBINARY(32)
SET @Salt = CRYPT_GEN_RANDOM(32)

DECLARE @Password NVARCHAR(255) = 'default'
DECLARE @PasswordBytes VARBINARY(32)

SET @PasswordBytes = HASHBYTES('SHA2_256', @Salt + CONVERT(VARBINARY(32), @Password))

INSERT INTO USERS (RoleId, Username, Salt, [Password], Email) VALUES (1, 'SuperAdmin', @Salt, @PasswordBytes, 'assistiva@assitiva.com')
GO

CREATE TABLE Disabilities(
    DisabilityId INT IDENTITY(1, 1) CONSTRAINT PK_Disabilities PRIMARY KEY NOT NULL,
    [Name] NVARCHAR(50) CONSTRAINT UQ_Disabilities_Name UNIQUE NOT NULL, -- No permite datos duplicados,
    [Description] NVARCHAR(255) NULL,
	IsActive BIT DEFAULT 1 NOT NULL
)
GO

INSERT INTO Disabilities ([Name], [Description])VALUES
	('Auditiva', 'Discapacidad que afecta la capacidad de oír, total o parcialmente. Requiere comunicación visual (lenguaje de señas, lectura labial) o dispositivos auditivos. Necesita accesibilidad en medios y entornos con subtítulos, intérpretes o señales visuales.'),
	('Motora', 'Dificultad para moverse, coordinar miembros o mantener el equilibrio. Incluye uso de sillas de ruedas, prótesis o adaptadores. Precise accesibilidad física (rampas, ascensores) y herramientas ergonómicas para autonomía en actividades cotidianas.'),
	('Visual', 'Pérdida total o parcial de la visión. Implica usar recursos como braille, lectores de pantalla, perros guía o aumentar el contraste. Requiere entornos con señalización táctil, auditiva y diseño inclusivo para navegación autónoma.')
GO

CREATE TABLE Students(
	StudentId INT IDENTITY(1, 1) CONSTRAINT PK_Students PRIMARY KEY NOT NULL,
	UserId INT NOT NULL,
	FirstName NVARCHAR(100) NOT NULL,
    PaternalLastName NVARCHAR(50) NOT NULL,
    MaternalLastName NVARCHAR(50) NULL,
	Gender NVARCHAR(10) NOT NULL,
    DateOfBirth DATE NOT NULL,
	EducationLevel NVARCHAR(20) NOT NULL,
    Profession NVARCHAR(50) NULL,
    ProfessionStatus NVARCHAR(10) NOT NULL,
    MaritalStatus NVARCHAR(15) NOT NULL,
    BloodType NVARCHAR(15) NOT NULL,
	Street NVARCHAR(75) NOT NULL,
    Number NVARCHAR(10) NOT NULL,
    Neighborhood NVARCHAR(50) NOT NULL,
    City NVARCHAR(30) NOT NULL,
    PostalCode INT NOT NULL,
    [State] NVARCHAR(30) NOT NULL,
    Country NVARCHAR(30) NOT NULL,
	PhotoUrl NVARCHAR(200) NULL,
    IsActive BIT DEFAULT 1 NOT NULL,
	CONSTRAINT FK_Students_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId)
)
GO

CREATE TABLE StudentDisabilities(
    Id INT IDENTITY(1, 1) CONSTRAINT PK_StudentDisabilities PRIMARY KEY NOT NULL,
    StudentId INT NOT NULL,
    DisabilityId INT NOT NULL,
    CONSTRAINT FK_StudentDisabilities_StudentId FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    CONSTRAINT FK_StudentDisabilities_DisabilityId FOREIGN KEY (DisabilityId) REFERENCES Disabilities(DisabilityId),
    CONSTRAINT UQ_Student_Disability UNIQUE (StudentId, DisabilityId)  -- No permite datos duplicados
)
GO

CREATE TABLE Classrooms(
	ClassroomId INT IDENTITY(1, 1) CONSTRAINT PK_Classrooms PRIMARY KEY NOT NULL,
	TeacherId INT NOT NULL,
	[Name] NVARCHAR(50) CONSTRAINT UQ_Classrooms_Name UNIQUE NOT NULL, -- No permite datos duplicados
	CONSTRAINT FK_Classrooms_TeacherId FOREIGN KEY (TeacherId) REFERENCES Users(UserId)
)
GO

CREATE TABLE ClassroomStudents(
	Id INT IDENTITY(1, 1) CONSTRAINT PK_ClassroomStudents PRIMARY KEY NOT NULL,
	ClassroomId INT NOT NULL,
	StudentId INT NOT NULL,
	CONSTRAINT FK_ClassroomStudents_ClassroomId FOREIGN KEY (ClassroomId) REFERENCES Classrooms(ClassroomId),
	CONSTRAINT FK_ClassroomStudents_StudentId FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
	CONSTRAINT UQ_Classroom_Student UNIQUE (ClassroomId, StudentId) -- No permite datos duplicados
)
GO

CREATE TABLE Subjects(
	SubjectId INT IDENTITY(1, 1) CONSTRAINT PK_Subjects PRIMARY KEY NOT NULL,
	Code NVARCHAR(10) CONSTRAINT UQ_Subjetcs_Code UNIQUE NOT NULL, -- No permite datos duplicados
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(255) NULL,
	IsActive BIT DEFAULT 1 NOT NULL
)
GO