# WebAPI
SmarterCity WebAPI Test

This is .Net Core 5 Web API.

This WebAPI is working with MSSQL Server. 
To create database, please run CreateDatabase.sql file on your MSSQL Server.

Please change ConnectionString in appsettings.json file to your MSSQL Server database.

You can run this application in MS Visual Studio 2019. 

The local address for example: https://localhost:44343/swagger/index.html

You may see a different port number when running on you local machine.
Please use your local address:port to update shared.service.ts file of FrontEnd application.

File: shared.service.ts 
readonly APIUrl = "https://localhost:44343/api";

Thank you.
