CREATE DATABASE shoeshop
use shoeshop
 /*tabela e produktit*/
 create table Product
 (
 ProductId int identity(1,1),
 ProductName varchar(500),
 Brand varchar(500),
 Color varchar(500),
 Size int,
 Price decimal,
 Quantity int,
 ImageName varchar(500)
 );

 insert into Product values('Shoe','Nike','Black','39','65.99','7','anonymous.png')


 select * from Product

 /*Tabela e kategorise*/
 create table Category
 (
 CategroyId int identity(1,1),
 CategoryName varchar(500),
 CategoryDescription varchar(500),
 ImageOfCategory varchar(500)
 );

 insert into Category values('Men-s shoes','Our Men-s Shoes category includes a wide selection of shoes for men,
 including dress shoes, casual shoes, boots, and athletic shoes. 
 Whether you are looking for something for work or play, we have got you covered.','categroy1.png');

