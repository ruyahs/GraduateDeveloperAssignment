create database GraduateDevAssignment ; 

create table TransactionType(

TransactionTypeID smallint identity primary key not null ,
TransactionTypeName nvarchar(50) not null 

);

create table Client (
ClientID int not null primary key identity ,
[Name] nvarchar(50) not null , 
Surname nvarchar(50) not null,
ClientBalance decimal(18,2) not null

)


create table [Transaction](

TransactionID bigint identity primary key not null ,
Amount decimal(8,2) not null , 
TransactionTypeId smallint not null foreign key references TransactionType(TransactionTypeID),
ClientID int not null foreign key references client(ClientID),
Comment nvarchar(100) 

)

insert into TransactionType values('Debit');
insert into TransactionType values('Credit');

insert into Client values 
('Peter', 'Parker', 100),
('Tony' , 'Stark' ,800000),
('Bruce','Banner', 254111);

insert into [Transaction] values 
(1000,1,1,'Winnings'),
(-500,2,3,'Losing'),
(-9000,2,2,'Losing') ; 
