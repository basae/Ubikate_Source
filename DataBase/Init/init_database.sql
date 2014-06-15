--drop database ubikatebd
create database ubikateBD;

go

use ubikateBD;

go

create table proprietary(
	proprietaryid int identity primary key,
	rfc varchar(15),
	lastname varchar(60),
	firstname varchar(60),
	username varchar(30),
	password varchar(40),
	subscribed bit
);

go

create table businessType(
businessTypeid int identity primary key,
name varchar(100),
urlIcon varchar(100)
);

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
foreign key(businessTypeid) references businessType,
foreign key(proprietaryid) references proprietary
);

go

create table HistorySearch
(
historyid int identity primary key,
datesearch datetime,
keyword varchar(300),
businessTypeid int,
foreign key(businessTypeid) references businessType
);
go