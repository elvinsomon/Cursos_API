CREATE DATABASE Cursos
Use Cursos

CREATE TABLE Estudiante
(
    IdEstudiante int primary key identity,
    Codigo varchar(10),
    Nombre varchar(50),
    Apellido varchar(50),
    NombreApellido as CONCAT(Nombre, ' ', Apellido),
    FechaNacimiento date
);

CREATE TABLE Periodo
(
    IdPeriodo INT primary key,
    Anio int,
    Estado bit
);

CREATE TABLE Matricula
(
    IdEstudiante int,
    IdPeriodo int,
    primary key(IdEstudiante, IdPeriodo),
    Fecha datetime
);

CREATE TABLE Curso
(
    IdCurso int primary key,
    Codigo varchar(10),
    Descripcion varchar(100),
    Estado bit
);

CREATE TABLE InscripcionCurso
(
    IdEstudiante int,
    IdPeriodo int,
    IdCurso int,
    Fecha datetime,
    PRIMARY KEY(IdEstudiante, IdPeriodo, IdCurso)
    
);

ALTER TABLE Matricula ADD FOREIGN KEY(IdEstudiante) REFERENCES Estudiante(IdEstudiante);
ALTER TABLE Matricula ADD FOREIGN KEY(IdPeriodo) REFERENCES Periodo(IdPeriodo);
ALTER TABLE InscripcionCurso ADD FOREIGN KEY(IdEstudiante, IdPeriodo) REFERENCES Matricula(IdEstudiante, IdPeriodo);
ALTER TABLE InscripcionCurso ADD FOREIGN KEY(IdCurso) REFERENCES Curso(IdCurso);