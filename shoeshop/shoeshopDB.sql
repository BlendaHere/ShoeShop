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


--Tabela e WishList-es

CREATE TABLE WishList (

  WishListId int identity(1,1), 
  ProductName VARCHAR (50)  NOT NULL ,
  ProductDescription VARCHAR(250),
  Price DECIMAL(10,2) NOT NULL
);

 insert into WishList
 values('Nike Air Max', 'Running shoes with air cushioning technology', 99.99)

select * from WishList
 
 --Tabela e Delivery
CREATE TABLE Delivery (
  DeliveryId int IDENTITY(1,1),
  OrderId int NOT NULL,
  DeliveryDate datetime NOT NULL,
  DeliveryAddress varchar(100) NOT NULL,
  DeliveryStatus varchar(50) NOT NULL
);

INSERT INTO Delivery (OrderId, DeliveryDate, DeliveryAddress, DeliveryStatus)
VALUES (1, '2023-05-05', ' Muharrem Fejza', 'Pending');

select * from Delivery


--Tabela e Blog-ut

CREATE TABLE Blog (
  BlogId INT IDENTITY(1,1),
  Title VARCHAR(100) NOT NULL,
  Author VARCHAR(50) NOT NULL,
  DatePosted DATE NOT NULL,
  Content TEXT NOT NULL,
  ImageName  VARCHAR(200)
);

INSERT INTO Blog (Title, Author, DatePosted, Content, ImageName )
VALUES ('First Blog Post', 'Era', '2022-05-16', 'Celebrity shoe styles: Get the look for less', 'blog1.png');
select * from Blog   


--Tabela e Inspiration 

CREATE TABLE Inspiration (
  PostId INT IDENTITY(1,1),
  Title VARCHAR(500) NOT NULL,
  DatePosted DATE NOT NULL,
  Description VARCHAR(500) NOT NULL,
  ImageName VARCHAR(100) NOT NULL
);

INSERT INTO Inspiration (Title, DatePosted, Description, ImageName)
VALUES ('Find the perfect pair of running shoes for your next marathon', '2023-05-05', 'When you want to feel like you are running on fluffy clouds, these shoes are the ones.', 'insp1.png')
select * from Inspiration


--Tabela e Staff-it

CREATE TABLE Staff (
  StaffId INT IDENTITY(1,1),
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  Email VARCHAR(100) UNIQUE NOT NULL,
  HireDate DATETIME NOT NULL,
  Position VARCHAR(100) NOT NULL,
  EmploymentStatus VARCHAR(10) NOT NULL,
  ImageName VARCHAR(100) NULL

);

INSERT INTO Staff (FirstName, LastName, Email, HireDate, Position, EmploymentStatus,ImageName)
VALUES ('Blendiona', 'Biqkaj', 'blendionabiqkaj3@gmail.com', '2022-01-01', 'Manager', 'Active','blendiona1.png');

select * from Staff

