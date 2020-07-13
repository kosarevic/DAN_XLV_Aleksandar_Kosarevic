--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblProduct')
drop table tblProduct

create table tblProduct
(
ProductID int primary key IDENTITY(1,1),
Name varchar(50),
Code varchar(50),
Amount int,
Price int,
Stored bit
)

