# Beginning Entity Framework Core 2.0 (2018)  
__By Derek J. Rouleau__    

## Chapter 1: Getting Started  
__Setting Up The Project__  
1. Create a new Console App (.NET Core) Application using Visual Studio
2. Right-click on the newly created Project > Manage NugGet Packages, Click on the Browse Tab.
3. Install the following Packages:  
  * Microsoft.EntityFrameworkcore.SqlServer v2.0.0
  * Microsoft.EntityFrameworkcore.Tools v2.0.0
  * Microsoft.EntityFrameworkcore.SqlServer.Design v2.0.0  

__Creating the Database and Tables (Entities) using Code First Approach__    
1. Add two new folders to the project:  `Models` and `Data`
2. Add a one or more model classes to the Models folder e.g `Book.cs`, `Category.cs`
3. Add a `DBContext` class in your `Data` folder e.g `AppDBContext.cs`
4. Click on `Tools` > `NuGet Package Manager` > `Package Manager Console`
5. In the Package Manager Console Enter the command  
```
> Add-Migration ComputerInventory.Data.AppDBContext
```  
See below for how to run similar command using `dotnet-ef` CLI tool.  
Where `ComputerInventory.Data.AppDBContext` fully defined name of the DB Context.     
6. If the `Add-Migrations` command is successful, some partial classes will be generated inside a `Migration` directory.  
Here you will find the Code responsible to the Database migration.
7. Now run the `Update-Database` command in the `Package Manager Console`  
```
> Update-Database
```
8. You should now have a Database which you can see using the SQL Server Object Explorer or a SQL Client like SQL Server Management Studio.  
Note that you may have to use the same login or one whose user is mapped to the database.  
9. You may issue the `Remove-Migration` command to undo the `Add-Migrations` result.

__Using dotnet-ef CLI__  
In the steps above we used the _Package Manager Console_ to run commands that generated our Database Migrations, Updated the Database etc. This steps can also be achieved using the `dotnet-ef` CLI which is a `dotnet` CLI tool.  
First install the `dotnet-ef` dotnet tool
```
> dotnet tool install --global dotnet-ef
```  
To install a specific version of `dotnet-ef`  
```
> dotnet tool install --global dotnet-ef --version 3.1.4
```
Check that the `dotnet-ef` tool was successfully installed:  
```
> dotnet-ef --version
```
Now to do Generate Database Migration from the Database Context, cd into the Project folder, not the solution folder but the project folder.
```
> dotnet-ef migrations add  ComputerInventory.Data.AppDBContext
```
Where `ComputerInventory.Data.AppDBContext` is the fully qualified name your database context class.  
To update the database
```
>  dotnet-ef database update
```  
And to remote a Migration
```
> dotnet-ef migrations remove
```

## Chapter 2: Working with Multiple Tables  
Return to this chapter pg 55:  Adding Data to a Table That Has Foreign Keys

## Chapter 6: ASP.NET MVC and EF Core 2.0
### Entity Framework Core  
EF Core does not support visual designer for DB model and wizard to create the entity and context classes the way EF 6 does it.  
So to generate the entities and DB context, you can either use Visual Studio's _Package Manager Console_ or `dotnet` CLI.  
#### DB First Approach
__Dependencies__  
The following packages are required   
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.EntityFrameworkCore.SqlServers

__Using VS Package Manager Console__   
1. Install the required packages
```
 > Install-Package Microsoft.EntityFrameworkCore.Tools
 > Install-Package Microsoft.EntityFrameworkCore.SqlServer
```
If your project is an older version than .net6 (e.g .net5 or lower) you may specify a compatible version as an argument using the `-Version` flag.
```
 > Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.17
 > Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.17
```  
2. Run the `Scaffold-DbContext` command
```
> Scaffold-DbContext "Server=CHUCKSM;Database=OjlinksDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```   
This will generate a DbContext class and models for all the tables in your database inside the Models directory.   
If your database requires a username and password for access, you can include them in your connection string.  
```
"Server=CHUCKSM;Database=OjlinksDB;Trusted_Connection=False;User Id=my_username;Password=my_password;"
```   
Remember to remove the username and password in the generated DbContext and not commit it to version control.  

__Useful Flags__
Here are some useful flag for the Scaffold-DbContext command in the Package Manager Console
* `-Force` to overwrite your existing DbContext and Entities.
* `-UseDatabaseNames` to retain your database column name case.
* `-Context` to specify the name you want for your DB Context

__Using dotnet CLI__  
Entity framework can  also be setup using `dotnet` and `dotnet-ef` CLI tools.  
1. Install the required packages from you terminal using `dotnet`
```
> dotnet add package Microsoft.EntityFrameworkCore.Tools
> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
If you project is an older version than .net6 (e.g .net5 or lower) you may specify a compatible version using the `--version` flag.  
```
> dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.17
> dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.17
```
2. Install `dotnet-ef` if you have not already done so.
```
 > dotnet tool install --global dotnet-ef
```  
3. Run the `dbcontext scaffold` sub-command using `dotnet-ef` dotnet tool.
```
> dotnet-ef dbcontext scaffold  "Server=CHUCKSM;Database=OjlinksDB;Trusted_Connection=False;User Id=db_user;Password=db_pass;" Microsoft.EntityFrameworkCore.SqlServer -o Data
```  
This will generate a DbContext class and models for all the tables in your database inside the Data directory.  

__Useful Flags__
Here are some useful flag for the `dotnet-ef` CLI tool
* `--force` to overwrite your existing DbContext and Entities.
* `--use-database-names` to retain your database column name case.
* `--project` to specify the project to be acted on
* `-c` to specify the name you want for your DB Context
