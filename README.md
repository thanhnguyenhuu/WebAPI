# WebAPI
SmarterCity WebAPI Test

This is .Net Core 5 Web API.

This WebAPI is working with MSSQL Server. 
To create database, please run CreateDatabase.sql file on your MSSQL Server.

Please change ConnectionString in appsettings.json file to your MSSQL Server database.

You can run this application in MS Visual Studio 2019. 

The local address for example: https://localhost:44343/swagger/index.html

You may see a different port number when running on you local machine.

Please use your local https://localhost:[port]/api to update shared.service.ts file of FrontEnd application.


File: shared.service.ts 

readonly APIUrl = "https://localhost:44343/api";

Example customer data for FrontEnd or Web API Post method:

{
  "id": 0,
  "firstName": "Thanh",
  "lastName": "Nguyen",
  "mobileNumber": "0412345678",
  "address1": "1/123 White Road",
  "address2": "",
  "suburb": "Bankstown",
  "state": "NSW",
  "postCode": "2200"
}


Thank you.
