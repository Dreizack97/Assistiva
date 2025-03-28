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
	('Auditiva', 'Discapacidad que afecta la capacidad de o�r, total o parcialmente. Requiere comunicaci�n visual (lenguaje de se�as, lectura labial) o dispositivos auditivos. Necesita accesibilidad en medios y entornos con subt�tulos, int�rpretes o se�ales visuales.'),
	('Motora', 'Dificultad para moverse, coordinar miembros o mantener el equilibrio. Incluye uso de sillas de ruedas, pr�tesis o adaptadores. Precise accesibilidad f�sica (rampas, ascensores) y herramientas ergon�micas para autonom�a en actividades cotidianas.'),
	('Visual', 'P�rdida total o parcial de la visi�n. Implica usar recursos como braille, lectores de pantalla, perros gu�a o aumentar el contraste. Requiere entornos con se�alizaci�n t�ctil, auditiva y dise�o inclusivo para navegaci�n aut�noma.')
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
    CONSTRAINT FK_StudentDisabilities_DisabilityId FOREIGN KEY (DisabilityId) REFERENCES Students(DisabilityId),
    CONSTRAINT UQ_Student_Disability UNIQUE (StudentID, DisabilityID)  -- No permite datos duplicados
)
GO