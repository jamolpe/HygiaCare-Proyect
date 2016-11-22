CREATE TABLE Urgencias
(
	id int IDENTITY (1,1) not null, 
	CantidadUsuarios int NULL, 
	Hospital int not null FOREIGN KEY REFERENCES Hospitales(id),
    CONSTRAINT PK_Urgencias PRIMARY KEY (id)
)
GO