--drop database ubikatebd
create database ubikateBD;

go

use ubikateBD;

go

create table usertype
(
usertypeid int identity primary key,
name varchar(100) unique
)

go

create table [user]
(
userid int primary key identity,
firstname varchar(30),
lastname varchar(30),
username varchar(50) unique,
[password] varchar(50),
regdate datetime,
usertype int,
active bit,
foreign key (usertype) references usertype
)

create table proprietary(
	proprietaryid int identity primary key,
	rfc varchar(15),
	lastname varchar(60),
	firstname varchar(60),
	subscribed bit,
	insertuser int,
	insertdate datetime,
	updateuser int,
	updatedate datetime,
	active bit,
	foreign key (insertuser) references [user],
	foreign key (updateuser) references [user],
);

go

create table businessType(
businessTypeid int identity primary key,
name varchar(100) unique,
urlIcon varchar(100)
);

go
create table businessOffer
(
	businessofferId int identity primary key,
	begindate datetime,
	enddate datetime,
	description varchar(max),
	insertuser int,
	insertdate datetime,
	updateuser int,
	updatedate datetime,
	active bit,
	foreign key (insertuser) references [user],
	foreign key (updateuser) references [user],
	
)
go
create table business
(
businessid int identity primary key,
name varchar(150),
number varchar(20),
street varchar(30),
city varchar(30),
country varchar(30),
proprietaryid int,
businessTypeid int,
latitude decimal(10,6),
longitude decimal(10,6),
active bit,
insertuser int,
insertdate datetime,
updateuser int,
updatedate datetime,
foreign key(businessTypeid) references businessType,
foreign key(proprietaryid) references proprietary,
foreign key (insertuser) references [user],
foreign key (updateuser) references [user],
);

go

create table HistorySearch
(
historyid int identity primary key,
datesearch datetime,
keyword varchar(300)
);
go
insert into usertype(name) values('Administrador'),('Super Usuario'),('Usuario')
insert into [user](firstname, lastname,username,[password], regdate, usertype, active) values('UbikateAdmin','.','admin','password',getdate(),1,1);
