insert into RolType
([Name], [Description],IsActive)
values
('Administrador','Access to all methods, management of users', 1)
('Operator','Posts, consults, lists recipes', 1)

select * from RolType


insert into Users
(IdRol, [UserName], InsertDate, IsActive, EncryptedPassword, EncryptedToken, TokenExpireDate, UserEmail, UserPhone)

values
(1, 'Celia', GETDATE(), 1, '$2a$11$V6c1zrNzHljeiIQ81bLaoeogagZWvr2JUkUs8CHmWzHYJ6T2l0S5q', '', GETDATE(), 'garciacastillacelia@gmail.com', '622680228')

--password asdasd2
select * from Users

insert into Categories
([Name])
values
('Pescado')

insert into Alergens

select * from Categories

select * from Ingredients

select * from Recipes

select * from Recipe_Ingredients