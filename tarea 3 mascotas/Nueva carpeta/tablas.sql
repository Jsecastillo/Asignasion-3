-- Crear base de datos ()
IF DB_ID('VeterinariaDB') IS NULL
BEGIN
    CREATE DATABASE VeterinariaDB;
END
GO

USE VeterinariaDB;
GO

-- Tabla dueños
IF OBJECT_ID('dbo.Duenos', 'U') IS NOT NULL DROP TABLE dbo.Duenos;
CREATE TABLE dbo.Duenos (
    DuenoId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Telefono NVARCHAR(50) NULL,
    Correo NVARCHAR(150) NULL,
    Direccion NVARCHAR(250) NULL
);

-- Tabla mascotas
IF OBJECT_ID('dbo.Mascotas', 'U') IS NOT NULL DROP TABLE dbo.Mascotas;
CREATE TABLE dbo.Mascotas (
    MascotaId INT IDENTITY(1,1) PRIMARY KEY,
    DuenoId INT NOT NULL,
    Nombre NVARCHAR(120) NOT NULL,
    Especie NVARCHAR(50) NULL,
    Raza NVARCHAR(80) NULL,
    FechaNacimiento DATE NULL,
    Peso DECIMAL(6,2) NULL,
    Alergias NVARCHAR(250) NULL,
    CONSTRAINT FK_Mascotas_Duenos FOREIGN KEY (DuenoId) REFERENCES dbo.Duenos(DuenoId)
);

-- Tabla visitas (historial clínico)
IF OBJECT_ID('dbo.Visitas', 'U') IS NOT NULL DROP TABLE dbo.Visitas;
CREATE TABLE dbo.Visitas (
    VisitaId INT IDENTITY(1,1) PRIMARY KEY,
    MascotaId INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Motivo NVARCHAR(250) NULL,
    Sintomas NVARCHAR(MAX) NULL,
    Diagnostico NVARCHAR(MAX) NULL,
    Tratamiento NVARCHAR(MAX) NULL,
    Veterinario NVARCHAR(150) NULL,
    CONSTRAINT FK_Visitas_Mascotas FOREIGN KEY (MascotaId) REFERENCES dbo.Mascotas(MascotaId)
);

-- Datos de ejemplo
INSERT INTO dbo.Duenos (Nombre, Telefono, Correo) VALUES ('Carlos Pérez', '8888-8888', 'carlos@mail.com');
INSERT INTO dbo.Mascotas (DuenoId, Nombre, Especie, Raza, FechaNacimiento, Peso) VALUES (1, 'Firulais', 'Perro', 'Criollo', '2018-05-10', 12.5);
INSERT INTO dbo.Visitas (MascotaId, Motivo, Sintomas, Diagnostico, Tratamiento, Veterinario) VALUES (1, 'Vacunación', 'Control anual', 'Ok', 'Vacuna antirrábica', 'Dra. Gómez');
GO


USE VeterinariaDB;
GO

SELECT * FROM Mascotas;
