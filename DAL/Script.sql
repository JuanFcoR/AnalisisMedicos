create database AnalisisDb
Go
Use AnalisisDb
Go
create table Analisis
(
	
	AnalisisId int primary key,
	Fecha date,
	UsuarioId int not null,
	TipoId int

)
Go

Create table Usuarios
(
	UsuarioId int Primary key identity,
	Nombres varchar(30),
	Email varchar(25),
	NivelUsuario varchar(15),
	Usuario varchar(15),
	Clave varchar(16),
	FechaIngreso datetime
);

create table TiposAnalisis
(
	TipoId int primary key,
	Descripcion varchar(60)
)
Go

create table AnalisisDetalle
(
	AnalisisId int primary key,
	TipoId int primary key,
	Resultado varchar(30)

)
Go

Create table Usuarios
(
	UsuarioId int Primary key identity,
	Nombres varchar(30),
	Email varchar(25),
	NivelUsuario varchar(15),
	Usuario varchar(15),
	Clave varchar(16),
	FechaIngreso datetime
);