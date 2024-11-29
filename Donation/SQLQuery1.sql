use pratice

create table userImage
(
userId int primary key identity,
userImage varchar(MAX),
);


create table admin_donation
(
id int primary key identity,
purpose varchar(255),
description varchar(MAX),
amount varchar(10),
Image varchar(MAX)
);


create table user_donation
(
id int primary key identity,
mail varchar(255),
amount varchar(10),
);


select * from admin_donation;


select * from main;



CREATE PROCEDURE sp_admin_donation
(
@type varchar(20),
@id int =null,
@purpose varchar(255)=null,
@description varchar(MAX)=null,
@amount varchar(10)=null,
@Image varchar(MAX)=null)
AS
BEGIN
    if @type='sp_insert'
  BEGIN
     insert into admin_donation (id,purpose,description,amount,Image) values(@id,@purpose,@description,@amount,@Image);
  END

if @type='sp_update'
  BEGIN
     update admin_donation set purpose=@purpose,description=@description,amount=@amount,Image=@Image where Id=@id;
  END

if @type='sp_delete'
  BEGIN
     delete from admin_donation where id=@id;
  END

if @type='sp_select'
  BEGIN
     select * from admin_donation ;
  END
END



CREATE PROCEDURE SPGetAdminDonation
AS  
BEGIN  
SELECT purpose,description,amount,Image from admin_donation  
END


exec SPGetAdminDonation