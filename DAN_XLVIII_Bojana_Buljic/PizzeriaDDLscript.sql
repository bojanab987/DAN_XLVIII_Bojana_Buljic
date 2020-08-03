
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Pizzeria')
CREATE DATABASE Pizzeria;
GO
USE Pizzeria

--Deleting tables and views, if they exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblOrderItem')
	drop table tblOrderItem;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblOrder')
	drop table tblOrder;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblMenu')
	drop table tblMenu;
IF EXISTS(select * FROM sys.views where name = 'vwOrderItem')
	drop view vwOrderItem;
IF EXISTS(select * FROM sys.views where name = 'vwOrder')
	drop view vwOrder;
IF EXISTS(select * FROM sys.views where name = 'vwMenu')
	drop view vwMenu;

--create tables
CREATE TABLE tblMenu(
	ItemId INT PRIMARY KEY IDENTITY(1,1),
	PizzaName VARCHAR(30) NOT NULL,
	Price INT NOT NULL
	);
CREATE TABLE tblOrder(
	OrderId INT PRIMARY KEY IDENTITY(1,1),
	JMBG CHAR(13) NOT NULL,		
	OrderDateTime SMALLDATETIME not null,
	TotalPrice int NOT NULL,
	OrderStatus VARCHAR(20) not null
);

--Create tblOrderItem which will contain list of ordered menu items
CREATE TABLE tblOrderItem(
ID int identity(1,1) PRIMARY KEY,
ItemId INT FOREIGN KEY REFERENCES  tblMenu(ItemId) ON DELETE SET NULL,
Quantity int NOT NULL,
OrderID int FOREIGN KEY REFERENCES tblOrder(OrderId) NOT NULL
);
GO
--create view Order Item
CREATE VIEW vwOrderItem
as
select i.*, m.PizzaName, m.Price, o.TotalPrice, o.JMBG
from tblOrderItem i
INNER JOIN tblMenu m
on i.ItemId= m.ItemId
INNER JOIN tblOrder o
on i.OrderID=o.OrderId;
go

create view vwMenu as
select * from tblMenu;
GO

create view vwOrder as
select * from tblOrder;
go

--Insert menu values into table
INSERT INTO tblMenu(PizzaName, Price)
VALUES ('Capricosa', 820), ('Venezia',950),('FrutiDiMari',1000),('Vegetariana',620),('QuatroStagione',990),('Margarita',600),('Proscuito',950);