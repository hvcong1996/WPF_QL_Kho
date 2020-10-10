Create database QuanLyKho
go

use QuanLyKho
go

create table Unit
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max)
)
go

create table Suplier
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Address nvarchar(max),
	Phone nvarchar(15),
	Email nvarchar(200),
	ContractDate DateTime,
	MoreInfo nvarchar(max)
)
go

create table Customer
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Address nvarchar(max),
	Phone nvarchar(15),
	Email nvarchar(200),
	ContractDate DateTime,
	MoreInfo nvarchar(max)
)
go

create table Product
(
	Id nvarchar(128) primary key,
	DisplayName nvarchar(max),
	IdUnit int not null,
	IdSuplier int not null,
	QRCode nvarchar(max),
	BarCode nvarchar(max)

	foreign key(IdUnit) references Unit(Id),
	foreign key(IdSuplier) references Suplier(Id)
)
go

create table AccountRole
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max)
)
go

create table Account
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	UserName nvarchar(100),
	Password nvarchar(max),
	IdRole int not null

	foreign key (IdRole) references AccountRole(Id)
)
go

create table Input
(
	Id nvarchar(128) primary key,
	InputDate DateTime
)
go

create table InputInfo
(
	Id nvarchar(128) primary key,
	IdInput nvarchar(128) not null,
	IdProduct nvarchar(128) not null,
	Count int,
	InputPrice float default 0,
	OutputPrice float default 0,
	Status nvarchar(max)

	foreign key (IdInput) references Input(Id),
	foreign key (IdProduct) references Product(Id)
)
go

create table Out_put
(
	Id nvarchar(128) primary key,
	OutputDate DateTime
)
go

create table OutputInfo
(
	Id nvarchar(128) primary key,
	IdOutput nvarchar(128) not null,
	IdProduct nvarchar(128) not null,
	IdInputInfo nvarchar(128) not null,
	IdCustomer int not null,
	Count int,
	Status nvarchar(max)

	foreign key (IdOutput) references Out_put(Id),
	foreign key (IdProduct) references Product(Id),
	foreign key (IdInputInfo) references InputInfo(Id),
	foreign key (IdCustomer) references Customer(Id)
)
go