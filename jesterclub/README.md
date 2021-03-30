# Jester Club Web Site
This is a multilayered social website about jokes. Please note that this project is in an early stage.

## Technical information
This is a multilayered application. The backend has 2 layers: one for the database services and one is for the RESTful service. It has 1 front end layer for the Angular Web client.

### The Database service layer
It has 2 services:
- jcsqlserverservice: SQL Server Services for manipulating the SQL Server database of the site. It's written in C#/.NET Core. It uses the Entity Framework Core (Code first approach) technology.
- jcmongodbservice: MongoDB Services  for manipulating the MongoDB database of the site. It's written in C#/.NET Core. It uses the MongoDB's C# driver.

### The RESTful service layer
This is the ASP.NET Core 5 Web API for the Jester Club Web Site. Depend on the configuration it uses the MongoDB Services or the SQL Server Services for data manipulation.

### The Web client layer
The web client side is written in Angular 11. 

