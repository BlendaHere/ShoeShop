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

 insert into Category values('Man-s shoes','Our Man-s Shoes category includes a wide selection of shoes for men,
 including dress shoes, casual shoes, boots, and athletic shoes. 
 Whether you are looking for something for work or play, we have got you covered.','categroy1.png');
 
 select *from Category

 /*Tabela per Brendet*/
 create table Brand
 (
 BrandId int identity (1,1),
 BrandName varchar(500),
 BrandDescription varchar(500),
 ImageOfLogo varchar(500),
 Website varchar (500)
 );

 insert into Brand values ('Nike','Nike is a multinational corporation that designs and develops athletic footwear, apparel, and accessories.
 Known for its innovative designs and high-quality products','nike.png','"https://www.nike.com"');

 select * from Brand
 
 /*tabela e Llogaris se Klientit*/

 create table ClientAccount
 (
 ClientId int identity (1,1),
 FirstName varchar(500),
 LastName varchar(500),
 Email varchar(500),
 Password varchar(500),
 Address varchar(500),
 City varchar(500),
 PostalCode varchar(500),
 PhoneNumber varchar(500),
 Gender varchar(500),
 );

 insert into ClientAccount 
 values('Anesa','Zhegrova','anesazhegrova@gmail.com','anesa1234','Dobraje e Madhe',
        'Lipjan','10000','049587512','Femer');

select * from ClientAccount


/*Tabela e porosive*/

create table ClientOrder
(
OrderId int identity(1,1),
OrderDate datetime ,
ClientId int,
ProductId int,
Quantity int,
Size varchar(500),
Color varchar(500),
Price decimal,
PaymentMethod varchar(500)
);
insert into ClientOrder values ('2023-5-5','1','1','5','38','Black','35.70','PayPal');


select * from ClientOrder

 

