Create database QL_DIENDANDUNG
------------------create table-----------------------------------
create table tbl_TypeOfAccount
(
	Id int identity(1,1),
	TypeName nvarchar(max),
	Constraint PK_ID_TYPEUSER primary key(Id)
)

create table tbl_Producer
(
	Id int identity(1,1),
	ProducerName nvarchar(max),
	PhoneNumber char(10),
	Location nvarchar(max),
	Mail varchar(max),
	ProducerImage varchar(max),
	Constraint PK_ID_Producer primary key(Id)
)

create table tbl_Customer
(
	Id int identity(1,1),
	UserName nvarchar(max),
	Birthday date,
	Gender bit,
	Address nvarchar(max),
	PhoneNumber char(10),
	Constraint PK_ID_CUSTOMER primary key(ID)
)

create table tbl_User
(
	Id int identity(1,1),
	UserName nvarchar(max),
	Birthday date,
	Gender bit,
	Address nvarchar(max),
	PhoneNumber char(10),	
	Status bit,
	Constraint PK_ID_USER primary key(ID)
)

create table tbl_Account
(
	UserName varchar(20),
	Password char(12),
	UserId int,
	AccountImage varchar(max),
	TypeOfAccountId int not null,
	Status bit,
	Constraint PK_USERNAME_ACCOUNT primary key(UserName),
	Constraint FK_TYPEUSER_ACOOUNT foreign key(TypeOfAccountId) references tbl_TypeOfAccount(Id),
	Constraint FK_USERIDID_ACOOUNT foreign key(UserId) references tbl_User(Id)
)
create table tbl_TypeOfProduct
(
	Id int identity(1,1),
	TypeName nvarchar(max),
	Constraint PK_ID_TYPEPRODUCT primary key(Id) 
)

create table tbl_Product
(
	Id int identity(1,1),
	ProductName nvarchar(max),
	NumberOfProduct int,
	ProductImage varchar(max),
	Cost decimal,
	ExpiryDate int,
	TypeId int,
	ProducerId int,
	Constraint PK_ID_PRODUCT primary key(Id),
	Constraint FK_Producer_PRODUCT foreign key(ProducerId) references tbl_Producer(Id),
	Constraint FK_TYPEID_PRODUCT foreign key(TypeId) references tbl_TypeOfProduct(Id),
)

create table tbl_Bill
(
	Id int identity(1,1),
	CustomerID int,
	PersionalID int,
	DateCreate date,
	SumMoney decimal,
	Status bit,
	Constraint PK_BILL primary key(Id),
	Constraint FK_CUSTOMERID_BILL foreign key(CustomerId) references tbl_Customer(Id),
	Constraint FK_PERSIONAL_BILL foreign key(PersionalId) references tbl_User(Id)
)

create table tbl_ProductOfBill
(
	BillId int,
	ProductId int,
	Number int,
	SumCost decimal,
	Constraint PK_PRODUCTOFBILL primary key(BillId, ProductId),
	Constraint FK_BILLID_PRODUCTOFBILL foreign key(BillId) references tbl_Bill(Id),
	Constraint FK_PRODUCTID_PRODUCTOFBILL foreign key(ProductId) references tbl_Product(Id)
)

create table tbl_Store
(
	Id int identity(1,1),
	ProductId int,
	Number int,
	Constraint PK_ID_STORE primary key(Id, ProductId),
	Constraint FK_PRODUCTID_STORE foreign key(ProductId) references tbl_Product(Id)
)
---------------------Databse-----------------------------------
--TypeUserName
insert into tbl_TypeOfAccount values
(N'Khách Hàng'),
(N'Nhân Viên Bán Hàng'),
(N'Nhân Viên Quản Lý'),
(N'Nhân Viên Kho');
--TypeProducts
insert into tbl_TypeOfProduct values 
(N'Ổ Cắm Điện'),
(N'Cầu Dao Điện'),
(N'Bóng Đèn Điện')
--Producers
insert into tbl_Producer values 
(N'Panasonic','0123456789',N'Lạc Long Quân','panasonic@gmail.com',null),
(N'Robot','0123456789',N'Lũy Bán Bích','Robot@gmail.com',null),
(N'Xiaomi','0123456789',N'Lũy Bán Bích','Xiaomi@gmail.com',null),
(N'Apex','0123456789',N'Lê Trọng Tấn','Apex@gmail.com',null)
--Products
insert into tbl_Product values 
(N'Ổ cắm có dây Panasonic WCHG28352 (3m)', 20, 'OCD01.jpg', 657000, 3, 1, 1),
(N'Ổ cắm có dây Panasonic WCHG24332W', 20, 'OCD02.jpg', 320000, 3, 1, 1),
(N'Ổ cắm có dây Panasonic WCHG2836 (3m)', 20, 'OCD03.jpg', 598000, 3, 1, 1),
(N'Ổ cắm có dây Panasonic WCHG28334 (3m)', 20, 'OCD04.jpg', 799000, 3, 1, 1),
(N'Ổ cắm 3 ổ 5 chấu SPECIAL 3S5', 20, 'OCD05.jpg', 120000, 3, 1, 2),
(N'Ổ cắm 2 ổ 5 chấu Special 2S5', 20, 'OCD06.jpg', 110000, 3, 1, 2),
(N'Ổ cắm 3 ổ 3 chấu MULTI 3S3', 20, 'OCD07.jpg', 99000, 3, 1, 2),
(N'Ổ cắm 2 ổ 3 chấu + USB MULTI 2S3U', 20, 'OCD08.jpg', 230000, 3, 1, 2),
(N'Bộ bảo vệ sốc điện và chống sét SP2200', 20, 'OCD09.jpg', 230000, 3, 1, 2),
(N'Ổ cắm điện 4 ổ 5 chấu SPECIAL 4S5', 20, 'OCD10.jpg', 150000, 3, 1, 2),
(N'Ổ cắm 2 ổ 5 chấu + USB SPECIAL 2S5U', 20, 'OCD11.jpg', 260000, 3, 1, 2),
(N'Ổ cắm 3 ổ 3 chấu kèm cổng sạc USB Multi 3S3U', 20, 'OCD12.jpg', 250000, 3, 1, 2),
(N'Bộ trễ bảo vệ thiết bị lạnh Robot DL10A', 20, 'OCD13.jpg', 180000, 3, 1, 2),
(N'Ổ cắm điện Xiaomi 3AC 3USB', 20, 'OCD14.jpg', 240000, 3, 1, 3),
(N'Ổ cắm điện Xiaomi Mi Power Strip', 20, 'OCD15.png', 440000, 3,1, 3),
(N'Ổ cắm điện Xiaomi Mi Strip', 20, 'OCD16.jpg', 259000, 3, 1, 3),
(N'Ổ cắm điện thông minh Xiaomi Mi Power Strip (3 ổ cắm, 3 USB)', 20, 'OCD17.jpg', 350000, 3, 1, 3),
(N'Ổ cắm điện APE-OC2-3RU', 20, 'OCD18.jpg', 220000, 3, 1, 4),
(N'Ổ cắm điện APE-OC5-4RD', 20, 'OCD19.jpg', 180000, 3, 1, 4),
(N'Ổ cắm điện APE-OC5-3RD', 20, 'OCD20.jpg', 170000, 3, 1, 4),
(N'Ổ cắm điện APE-OC1-3R', 20, 'OCD21.jpg', 135000, 3, 1, 4),
(N'Ổ cắm điện APE-OC5-3R', 20, 'OCD22.jpg', 132000, 3, 1, 4),
(N'Ổ cắm điện APE-OC2-3R', 20, 'OCD23.jpg', 150000, 3, 1,4),
(N'Ổ cắm điện Apex APE-OC1-5RD', 20, 'OCD24.png', 190000, 3, 1, 4),
(N'Cầu dao điện 20A - vp-CB20A', 25, 'CDD01.jpg', 30000, 4, 2, 1),
(N'Cầu dao điện 2P25A Vanlock gắn nổi hoặc âm tường', 25, 'CDD02.jpg', 45000, 4, 2, 1),
(N'Cầu Dao điện 1 pha 16A', 25, 'CDD03.jpg', 69000, 4, 2, 1),
(N'Cầu dao điện (APTOMAT) Át khối 3 Pha MCCB 3P HIMEL HDM3630N 700-800A 70kA', 25, 'CDD04.jpg', 6055000, 4, 2, 1),
(N'Cầu dao chống giật điện 2P 16A', 25, 'CDD05.jpg', 255000, 4, 2, 1),
(N'Cầu dao điện DC 1000v-32A', 25, 'CDD06.jpg', 99000, 4, 2, 1),
(N'Cầu dao điện 30A 2 Pha đảo chiều', 25, 'CDD07.jpg', 52000, 4, 2, 1),
(N'Cầu dao điện Điện Quang ĐQ-M1636C20', 25, 'CDD08.jpg', 30000, 4, 2, 1),
(N'Cầu dao điện APTOMAT Át tép MCB 1P Schneider EZ9F 1P 40A 4.5kA ', 25, 'CDD09.jpg', 85000, 4, 2, 1),
(N'Cầu dao điện APTOMAT HIMEL MCB 2P 80-125A 10kA ', 25, 'CDD10.jpg', 310000, 4, 2, 1),
(N'Cầu dao điện 3 pha 20A 18kA', 25, 'CDD11.jpg', 690000, 4, 2, 2),
(N'Cầu dao điện 3 pha 50A 18kA', 25, 'CDD12.jpg', 920000, 4, 2, 2),
(N'Cầu dao điện 3 pha 30A 22kA', 25, 'CDD13.jpg', 920000, 4, 2, 2),
(N'Cầu dao điện 3 pha 60A 22kA', 25, 'CDD14.jpg', 920000, 4, 2, 2),
(N'Cầu dao điện APTOMAT Át tép MCB 4P Schneider EZ9F 4P 50 - 63A 4.5kA', 25, 'CDD15.jpg', 1160000, 4, 2, 2),
(N'MCCB 3P Schneider Electric Breaker Easy Pact Type EZC100B 3P 60A 7.5kA - EZC100B 3P 60A 7.5kA', 25, 'CDD16.jpg', 1160000, 4, 2, 3),
(N'CẦU DAO ĐIỆN ROCKY', 25, 'CDD17.jpg', 25000, 4, 2, 3),
(N'Cầu dao điện 20A - vp-CB20A', 25, 'CDD18.jpg', 15000, 4, 2, 3),
(N'Cầu Dao Điện Dân Dụng 5A - 10A - 20A - 50A', 25, 'CDD19.jpg', 30000, 4, 2, 3),
(N'Cầu dao điện cb cóc novip', 25, 'CDD20.jpg', 25000, 4, 2, 3),
(N'Cầu Dao Điện Tự Động National 30A', 25, 'CDD21.jpg', 25000, 4, 2, 4),
(N'Cầu dao điện tự động SAMMAX 40A', 25, 'CDD22.jpg', 30000, 4, 2, 4),
(N'Cầu dao điện 2pha20A 600V', 25, 'CDD23.jpg', 35000, 4, 2, 4),
(N'Cầu dao điện 2 pha 15A/600V', 25, 'CDD24.jpg', 22000, 4, 2, 4),
(N'Cầu dao điện an toàn sino', 25, 'CDD25.jpg', 36000, 4, 2, 4),
(N'Bóng đèn điện quang 3w', 15, 'BDD01.jpg', 30000, 1, 3, 1),
(N'Bóng đèn Điện Quang 3u_8w-14w-18w', 15, 'BDD02.jpg', 45000, 1, 3, 1),
(N'Bóng Đèn Điện Quang 24W', 15, 'BDD03.jpg', 44000, 1, 3, 1),
(N'Bóng đèn Điện quang 18 W 3U', 15, 'BDD04.jpg', 45000, 1, 3, 1),
(N'Bóng Đèn Điện Quang DowbleWing FPL 36W', 15, 'BDD05.jpg', 40000, 1, 3, 1),
(N'Bảng đèn LED trong xe hơi 48 bóng đèn điện áp 12V', 15, 'BDD06.jpg', 40000, 1, 3, 1),
(N'Bóng đèn điện led 5w OKAS', 15, 'BDD07.jpg', 40000, 1, 3, 2),
(N'Bóng đèn điện tử 6h3n eb', 15, 'BDD08.jpg', 100000, 1, 3, 2),
(N'Bóng đèn điện tử 5654W - 6AK5 - 6j1', 15, 'BDD09.jpg', 400000, 1, 3, 2),
(N'Chấn lưu bóng đèn điện tử 220-240V AC 36W điện áp rộng T8', 15, 'BDD10.jpg', 55000, 1, 3, 2),
(N'Bóng đèn điện led 12w Newstar', 15, 'BDD11.jpg', 45000, 1, 3, 2),
(N'Bóng đèn điện led 12w OKAS Siêu tiết kiệm điện AT12', 15, 'BDD12.jpg', 70000, 1, 3, 3),
(N'Bóng đèn điện quang 55w', 15, 'BDD13.jpg', 145000, 1, 3, 3),
(N'ELECTIC START Bóng đèn điện đa năng', 15, 'BDD14.jpg', 100000, 1, 3, 3),
(N'Bóng đèn Điện Quang Double Wing 36W', 15, 'BDD15.jpg', 290000, 1, 3, 3),
(N'Bộ bóng đèn Điện Quang Double Wing 36W', 15, 'BDD16.jpg', 190000, 1, 3, 3),
(N'Bóng đèn điện led 36w OKAS', 15, 'BDD17.jpg', 170000, 1, 3, 3),
(N'Bóng Đèn Điện đui xoáy 22w Kèm Dây Cắm dài 5m', 15, 'BDD18.jpg', 70000, 1, 3, 3),
(N'Bóng đèn tích điện có điều khiển', 15, 'BDD19.jpg', 100000, 1, 3, 4),
(N'Đèn led siêu sáng tiết kiệm điện 28w', 15, 'BDD20.jpg', 100000, 1, 3, 4),
(N'Đèn led siêu sáng tiết kiệm điện 32w', 15, 'BDD21.jpg', 100000, 1, 3, 4),
(N'Đèn led siêu sáng tiết kiệm điện 32w', 15, 'BDD22.jpg', 100000, 1, 3, 4),
(N'Đèn led siêu sáng tiết kiệm điện 20w', 15, 'BDD23.jpg', 100000, 1, 3, 4),
(N'Bóng đèn tích điện năng lượng mặt trời TYN003-12W', 15, 'BDD24.jpg', 100000, 1, 3, 4),
(N'Bóng đèn thủy ngân Iwasaki Điện 400 W HF 400 X', 15, 'BDD25.jpg', 450000, 1, 3, 4)

-------------------------Customer----------------------------
SET DATEFORMAT DMY;
insert into tbl_Customer values
(N'Nguyển Văn A','20/08/1999','True',N'HCM',0833184732),
(N'Nguyển Văn B','30/07/1999','False',N'Cần Thơ',0833184732),
(N'Nguyển Văn C','01/01/2002','False',N'Trà Vinh',0833184732)
-------------------------User----------------------------
SET DATEFORMAT DMY;
insert into tbl_User values
(N'Trịnh Nhật Hào','20/08/1999','True',N'HCM',0833184732,'True'),
(N'Bé Mờ','30/07/1999','False',N'Cần Thơ',0833184732,'True'),
(N'Tiểu Nha Đầu','01/01/2002','False',N'Trà Vinh',0833184732,'True'),
(N'Lương Minh A','30/07/1999','True',N'Tây Môn',0833184732,'True'),
(N'Nguyễn Minh Anh','01/01/2002','True',N'Hòa Bình',0833184732,'True')

-----------------------Acount----------------------------
insert into tbl_Account values
('admin','123456', 1, null, 3, 'True'),
('nvbh','123456', 2, null, 2, 'True'),
('nvk','123456', 3, null, 4, 'True')

-----------------------Bill----------------------------
SET DATEFORMAT DMY;
insert into tbl_Bill values
(1, 1, '10/08/2020', 120000000, 'True'),
(1, 2, '30/08/2020', 120000000, 'False')

-----------------------Store----------------------------
insert into tbl_Store values
(1, 14),
(2, 10)

-----------------------ProductOfBill----------------------------
insert into tbl_ProductOfBill values
(1, 1,2,null),
(2, 1,3,null),
(1,8,15,null)

-------------------Trigger------------------------------
--------SumCost of table tble_ProductOfBill---------
CREATE TRIGGER TRG_THANHTIEN ON tbl_ProductOfBill
FOR INSERT, UPDATE AS
BEGIN
	update tbl_ProductOfBill set SumCost = ct.Number* sp.Cost from inserted i,tbl_ProductOfBill ct, tbl_Product sp where ct.ProductId = sp.Id AND ct.BillId = i.BillId 
END

--------SumMoney of table tbl_Bill---------
CREATE TRIGGER TRG_TONGTIEN ON tbl_ProductOfBill
FOR INSERT, UPDATE AS
BEGIN
	update tbl_Bill set SumMoney=(select sum(ct.SumCost) from tbl_ProductOfBill ct where ct.BillId = i.BillId) from inserted i, tbl_Bill hd where hd.Id = i.BillId
END

------------------Selected------------------------
select * from tbl_Product
select * from tbl_Producer
select * from tbl_Account
select * from tbl_Bill
select * from tbl_Store
select * from tbl_TypeOfProduct
select * from tbl_TypeOfAccount
select * from tbl_User
<<<<<<< HEAD
select * from tbl_ProductOfBill
-----------------Delete ----------------------------
--delete from tbl_Product
--delete from tbl_TypeOfProduct
--delete from tbl_Account
--delete from tbl_User
--delete from tbl_ProductOfBill
-----------------------------------------------
select * from tble_ProductOfBill
--- f4d6746c2b507e8339a4c240cf94facafb508e2b
