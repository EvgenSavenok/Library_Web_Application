# Library web application 

This repository contains task for internship in Modsen, software development company. 

# Installation
### 1) Clone this repository to your machine.
### 2) To run application you need to install one of the latest versions of PostgreSQL database.
### 3) Using command prompt go the solution directory and write some commands.
This will create Migrations folder in project Entities and add new migration files there.
```bash
dotnet ef migrations add NameOfMigration --project Entities --startup-project Library_Web_Application
```
This command will create database according to the last migration:
```bash
dotnet ef database update --project Entities --startup-project Library_Web_Application
```
After that database will appear on your machine and you will can run this application through exe-file or IDE.

# Description

1) You can register as administrator or user. All usernames and e-mails must be unique, password cannot contain less than 10 symbols.
2) To reserve a books you need to add this books to your library via administrator role.
3) Amount of each book's copy cannot contain symbols and all fields in any add/edit form cannot be empty.
4) To run Unit tests you should open IDE (for example, JetBrains Rider) and run methods that you want in Test class using tools of IDE.

# Questions
1) The terms of reference say that I only need to make a book reservation, but nothing is said about implementing the return function. Is it necessary to implement this function?
2) Is it necessary to check the validity of the phone number and other fields that are not directly related to the implementation of task functions?
