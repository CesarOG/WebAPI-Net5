CREATE TABLE Producto
(
Id int identity(1,1) primary key not null,
Nombre varchar(100),
Tipo varchar(100),
Precio decimal(9,2)
);

CREATE TABLE Usuario
(
Id int identity(1,1) primary key not null,
NombreUsuario varchar(100),
Clave varchar(100),
Token varchar(100)
);
insert into Usuario Values ('cortega','12334','');