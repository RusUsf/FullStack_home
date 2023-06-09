--Check for database 
select version(),current_schema(),current_database(),current_user;

--Create database
create database monkey_db;

--Create table
create table monkeytable (id int primary key, tip varchar);

--Insert values
insert into monkeytable(id, tip) values
(1,'Do not give up after first failure!'),
(2,'Get over disappointments quickly!'),
(3,'Celebrate small accomplishments!');

--Create user
create user burner1 with password 'burner1' valid until 'Jan 1, 2024';

--Grant privileges
grant all privileges on all tables in schema public to burner;
grant all privileges on database monkey_db to burner;
grant all privileges on all sequences in schema public to burner;


