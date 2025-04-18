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

CREATE TABLE Subjects(
	SubjectId INT IDENTITY(1, 1) CONSTRAINT PK_Subjects PRIMARY KEY NOT NULL,
	Code NVARCHAR(10) CONSTRAINT UQ_Subjetcs_Code UNIQUE NOT NULL, -- No permite datos duplicados
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(255) NULL,
	IsActive BIT DEFAULT 1 NOT NULL
)
GO

INSERT INTO Subjects (Code, [Name], [Description], IsActive) VALUES
('AMABSA-001', 'Aritmética básica', 'Materia fundamental que cubre las operaciones básicas: suma, resta, multiplicación y división. Es la base para todos los demás conceptos matemáticos en educación primaria. Incluye el entendimiento de números, sus relaciones y operaciones fundamentales.', 1),
('FACOES-002', 'Fracciones', 'Estudio de las fracciones y sus operaciones. Cubre suma, resta, multiplicación y división de fracciones con igual y diferente denominador. Esencial para entender partes de un todo y conceptos más avanzados de matemáticas en grados superiores.', 1),
('GMABSA-003', 'Geometría básica', 'Introducción a las formas geométricas, cálculo de perímetros, áreas y volúmenes de figuras básicas como cuadrados, rectángulos, triángulos, círculos y cuerpos tridimensionales. Desarrolla el pensamiento espacial en estudiantes de primaria.', 1),
('MDICON-003', 'Medición', 'Conversión entre unidades de medida comunes: longitud (cm, m, km), masa (g, kg), volumen (ml, l) y tiempo (minutos, horas). Enseña a los estudiantes a manejar diferentes sistemas de medición en situaciones cotidianas.', 1)
GO

CREATE TABLE Formulas(
	FormulaId INT IDENTITY(1, 1) CONSTRAINT PK_Formulas PRIMARY KEY NOT NULL,
	SubjectId INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	Content NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	CONSTRAINT FK_Formulas_Subjects FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId)
)
GO

INSERT INTO Formulas (SubjectId, [Name], Content, [Description]) VALUES
(1, 'Suma', '$a + b = c$', 'Operación básica que combina dos cantidades (a y b) para obtener un total (c). Representa la unión de grupos o valores en una sola cantidad.'),
(1, 'Resta', '$a - b = c$', 'Operación que calcula la diferencia entre dos cantidades (a como valor inicial y b como sustraendo), resultando en c. Útil para determinar cuánto queda tras remover una parte.'),
(1, 'Multiplicación', '$a \times b = c$', 'Proceso de sumar un número (a) repetidamente (b veces) para obtener un producto (c). Ideal para calcular totales en grupos iguales.'),
(1, 'División', '$a \div b = c$', 'Operación que distribuye una cantidad (a) en partes iguales (b partes), obteniendo el valor de cada porción (c).'),
(2, 'Suma de fracciones con igual denominador', '$\frac{a}{c} + \frac{b}{c} = \frac{a + b}{c}$', 'Cuando dos fracciones comparten denominador (c), se suman directamente los numeradores (a + b), manteniendo el mismo denominador en el resultado.'),
(2, 'Resta de fracciones con igual denominador', '$\frac{a}{c} - \frac{b}{c} = \frac{a - b}{c}$', 'Similar a la suma, pero restando los numeradores (a - b) cuando los denominadores (c) son iguales. El denominador permanece constante.'),
(2, 'Suma de fracciones con diferente denominador', '$\frac{a}{b} + \frac{c}{d} = \frac{a \times d + c \times b}{b \times d}$', 'Para sumar fracciones con denominadores distintos (b y d), se convierte a un común denominador (b × d) sumando los productos cruzados de numeradores.'),
(2, 'Resta de fracciones con diferente denominador', '$\frac{a}{b} + \frac{c}{d} = \frac{a \times d - c \times b}{b \times d}$', 'Procedimiento análogo a la suma: se restan los productos cruzados de numeradores tras homogenizar denominadores (b × d).'),
(2, 'Multiplicación de fracciones', '$\frac{a}{c} \times \frac{b}{d} = \frac{a \times b}{c \times d}$', 'Se multiplican los numeradores (a × b) y los denominadores (c × d) directamente. El resultado es una fracción simplificada.'),
(2, 'División de fracciones', '$\frac{a}{c} \div \frac{b}{d} = \frac{a}{c} \times \frac{d}{b} = \frac{a \times d}{c \times b}$', 'Para dividir, se multiplica la primera fracción por la inversa de la segunda (d/b). Esto convierte la división en una multiplicación.'),
(3, 'Perímetro de un cuadrado', '$P = 4 \times lado$', 'Suma de las longitudes de sus cuatro lados iguales. El perímetro es la distancia total alrededor del cuadrado.'),
(3, 'Perímetro de un rectángulo', '$P = 2 \times (largo + ancho)$', 'Calcula la longitud total alrededor del rectángulo sumando dos veces su largo y dos veces su ancho.'),
(3, 'Perímetro de un triángulo', '$P = lado_1 + lado_2 + lado_3$', 'Suma de las longitudes de sus tres lados. Aplica a cualquier tipo de triángulo (equilátero, isósceles o escaleno).'),
(3, 'Circunferencia de un círculo', '$C = 2 \times \pi \times r$', 'Longitud del borde de un círculo, donde r es el radio y p ˜ 3.1416. Relaciona directamente el radio con la circunferencia.'),
(3, 'Área de un cuadrado', '$A = lado \times lado$', 'Mide la superficie dentro del cuadrado, calculada elevando al cuadrado la longitud de uno de sus lados.'),
(3, 'Área de un rectángulo', '$A = largo \times ancho$', 'Superficie cubierta por el rectángulo, obtenida multiplicando sus dimensiones longitudinales.'),
(3, 'Área de un triángulo', '$A = \frac{base \times altura}{2}$', 'Representa la mitad del producto de la base por la altura. Aplica a triángulos de cualquier forma.'),
(3, 'Área de un círculo', '$A = \pi \times r^2$', 'Superficie dentro del círculo, dependiente del cuadrado del radio (r) y la constante p.'),
(3, 'Área de un rombo', '$A = \frac{\text{diagonal mayor} \times \text{diagonal menor}}{2}$', 'Calcula la superficie mediante el producto de sus diagonales dividido entre dos.'),
(3, 'Volumen de un cubo', '$V = lado \times lado \times lado$', 'Espacio ocupado por el cubo en tres dimensiones, elevando al cubo la longitud de un lado.'),
(3, 'Volumen de un prisma rectangular (caja)', '$V = largo \times ancho \times alto$', 'Calcula la capacidad multiplicando sus tres dimensiones: longitud, anchura y altura.'),
(3, 'Volumen de un cilindro', '$V = \pi \times r^2 \times altura$', 'Espacio ocupado por el cilindro, combinando el área de su base circular (pr²) y su altura.'),
(4, 'De centímetros a metros', '$metros = \frac{centímetros}{100}$', 'Conversión de unidades de longitud: dividir entre 100, ya que 1 metro equivale a 100 centímetros.'),
(4, 'De metros a kilómetros', '$kilómetros = \frac{metros}{1000}$', 'Para distancias largas: dividir metros entre 1000, pues 1 kilómetro contiene 1000 metros.'),
(4, 'De kilómetros a metros', '$metros = kilómetros \times 1000$', 'Convertir kilómetros a metros multiplicando por 1000. Ejemplo: 2 km = 2000 m.'),
(4, 'De gramos a kilogramos', '$kilogramos = \frac{gramos}{1000}$', 'Unidad mayor de masa: dividir gramos entre 1000. 1 kilogramo = 1000 gramos.'),
(4, 'De kilogramos a gramos', '$gramos = kilogramos \times 1000$', 'Convertir kilogramos a gramos multiplicando por 1000. Útil en mediciones precisas.'),
(4, 'De mililitros a litros', '$litros = \frac{mililitros}{1000}$', 'Volumen en litros: dividir mililitros entre 1000. 1 litro = 1000 mililitros.'),
(4, 'De litros a mililitros', '$mililitros = litros \times 1000$', 'Conversión a unidades menores: multiplicar litros por 1000 para obtener mililitros.'),
(4, 'De minutos a horas', '$horas = \frac{minutos}{60}$', 'Transformar tiempo a horas dividiendo minutos entre 60. Ejemplo: 120 minutos = 2 horas.'),
(4, 'De horas a minutos', '$minutos = horas \times 60$', 'Convertir horas a minutos multiplicando por 60. 1 hora = 60 minutos.')
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

CREATE TABLE ClassroomSubjects(
	Id INT IDENTITY(1, 1) CONSTRAINT PK_ClassroomSubjects PRIMARY KEY NOT NULL,
	ClassroomId INT NOT NULL,
	SubjectId INT NOT NULL,
	CONSTRAINT FK_ClassroomSubjects_ClassroomId FOREIGN KEY (ClassroomId) REFERENCES Classrooms(ClassroomId),
	CONSTRAINT FK_ClassroomSubjects_SubjectId FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId),
	CONSTRAINT UQ_Classroom_Subject UNIQUE (ClassroomId, SubjectId) -- No permite datos duplicados
)
GO