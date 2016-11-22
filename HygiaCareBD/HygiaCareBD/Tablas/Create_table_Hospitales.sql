-- =========================================
-- Create table template Windows Azure SQL Database 
-- =========================================


CREATE TABLE Hospitales
(
	id int IDENTITY (1,1) not null, 
	Nombre char(10) NULL, 
	Comunidad char(25) NULL,
    CONSTRAINT PK_Hospitales PRIMARY KEY (id)
)
GO
