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
