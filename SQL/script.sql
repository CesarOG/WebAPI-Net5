CREATE TABLE Productos
(
Id int identity(1,1) primary key not null,
Nombre varchar(100),
Tipo varchar(100),
Precio decimal(9,2)
);

CREATE TABLE Usuarios
(
Id int identity(1,1) primary key not null,
NombreUsuario varchar(100),
Clave varchar(100),
Token varchar(100)
);
insert into Usuarios Values ('cortega','12345','');


CREATE TABLE ROL
(
	Id_Rol  UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Nombre VARCHAR(30),
	Estado BIT,
	FechaCreacion DATETIME
);

INSERT INTO ROL VALUES (DEFAULT,'ADMINISTRADOR',1,GETDATE());
INSERT INTO ROL VALUES (DEFAULT,'VENDEDOR',1,GETDATE());
SELECT * FROM ROL