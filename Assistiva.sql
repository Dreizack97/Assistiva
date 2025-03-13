CREATE DATABASE Assistiva
GO

USE Assistiva
GO

CREATE TABLE Roles(
    RoleId INT IDENTITY(1, 1) CONSTRAINT PK_Roles PRIMARY KEY NOT NULL,
    [Name] NVARCHAR(25) NOT NULL
)
GO

INSERT INTO Roles([Name]) VALUES ('Administrador'), ('Director'), ('Docente'), ('Auxiliar'), ('Estudiante')
GO

CREATE TABLE Users(
    UserId INT IDENTITY(1, 1) CONSTRAINT PK_Users PRIMARY KEY NOT NULL,
    RoleId INT NOT NULL,
    Username NVARCHAR(50) CONSTRAINT UQ_Users_Username UNIQUE NOT NULL, --No permite datos duplicados
    Salt VARBINARY(32) NOT NULL,
    [Password] VARBINARY(32) NOT NULL,
    Email NVARCHAR(100) CONSTRAINT UQ_Users_Email NOT NULL, --No permite datos duplicados
    UrlPicture NVARCHAR(200) NULL,
    RecoveryCode NVARCHAR(16) NULL,
    ExpirationCode DATETIME NULL,
    IsPasswordReset BIT NULL DEFAULT 0,
    LastPasswordReset DATETIME NULL,
    IsPasswordDefect BIT DEFAULT 1 NOT NULL,
    LastPasswordChange DATETIME NOT NULL,
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

INSERT INTO USERS (RoleId, Username, Salt, [Password], LastPasswordChange) VALUES (1, 'SuperAdmin', @Salt, @PasswordBytes, GETDATE())
GO