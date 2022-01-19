# Mastering Entity Framework (2015)  
__By Rahul Rajat Singh__  
[GitHub Code](https://github.com/PacktPublishing/Mastering-Entity-Framework)  

## Chapter 1: Introduction to Entity Framework   
__Understanding the Entity Data Model__  
The Entity Data Model (EDM) defines the conceptual model classes, the relationship between those classes, and the mapping of those models to the database schema.     
Once our EDM is create, we can perform all the CRUD operations on our conceptual model, and Entity Framework will translate all these object queries to database queries (SQL).  

__Understanding the ObjectContext class__  
The _ObjectContext_ class is the main object in Entity Framework. It is responsible for:
* Managing database connection
* Providing support to perform CRUD operations
* Keeping track of changes in the models so that the models can be updated in the database.  

__The DbContext__
The _DbContext_ class is a wrapper on top of the _ObjectContext_ class. It is a newer API and it provided  a better API to manage database connections and perform CRUD operations.

__Development styles__  
1. __Database First:__ This approach is often used where there is already an exiting table or for data centric application
2. __Code First:__ This approach is best suited for domain-centric application.  
3. __Model First:__ It is similar to the Code First approach but it uses a visual EDM designer to design the models.

__Entity Framework Database First approach__  
First, you add a new ADO.NET EDM to your project:
* Right click on the project
* Click on Add > New Item
* Select _Data_ and _ADO.NET Entity Data Model_
* Enter a name for you EDM (preferable the project name) and click Add.
* Choose the _EF Designer from database_ option and click Next.
* Create a New Connection or paste your connection string in the box if you already have one.
* Next > Choose Entity Framework version
* Next > Select you database objects, Enter Model Namespace
* Click Finish.
* After some ok clicks you should see the `.edmx` file which represents your EDM.

In the database first approach, the Entity Model is able to incrementally update the conceptual model if the database schema is updated. We just need to right-click on the visual designer and chose _Update Model from Database_.  

__Entity Framework Model First approach__   
Using the model first approach we create our models directly in the Visual Entity Designer, and then create the database schema based on our model.  
* Right-click on your project
* Click on Add >  New Item  
* Select _Data_ and _ADO.NET Entity Data Model_
* Enter a name for your EDM (preferable the project name) and  click Add
* Choose the _Empty EF Designer model_ option and click Finish.
* This generates the `.edmx` file. You can then use the visual entity designer to add entities and relationships.  
* To add an Entity, simply drag _Entity_ from the Toolbox
* You can then add properties to the Entity: right-click on the Entity > Add New > Scalar Property.  
* You can change the Type of the property using the property bar after selecting the property.   
* After the model have been create you right-click on it and click _Generate Database from Model_ .
* Click on _Create New Connection_ and enter DB credential to generate a connection string > Next
* Select Entity Framework version > Next
* You may save the Entity Designer Script
* Click Finish   
* A SQL script will be generated which you must run to create the database and table(s).

For the Model First approach, one important thing to consider is that the incremental changes in our model will not perform the incremental updated to the database. Rather, it will give us the SQL to create the updated database form the ground up.

If you want to perform incremental updated to the database, use the [Database Generation Power Pack](https://marketplace.visualstudio.com/items?itemName=AdiUnnithan.EntityDesignerDatabaseGenerationPowerPack)

__Entity Framework Code First approach__   
The Entity Framework Code First approach enables us to write simple C# classes, that is, _Plain Old CLR Objects (POCOs)_ for our models, and then lets use persis them in a data store by defining a `DbContext` class for our model classes.
* Install Entity Framework Package in your project using the NuGet Package Manager, the Package Manager Console or Dotnet CLI.
* Create your Model class with properties
* Create you Db Context class which should be a partial class that extends `System.Data.Entity.DbContext`

__Possible Errors__  
__Error__  
error 3024: Problem in mapping fragments starting at line 6:Must specify mapping for all key properties (Todos.Id, Todos.TodoItem, Todos.IsDone) of the EntitySet Todos.  
__Solution__  
* Right-click on the EDM file (Filename.edmx), click on _Open with_  
* Select XML (Text) Editor and click OK
* Find the _EntitySetMapping_ element for the Model type and make sure that all the properties Names are mapped to the correct Column name for the table

__Error__  
The number of primary key values passed must match number of primary key values defined on the entity.  
__Solution__
* Make sure your model have only one primary key.

__Error__   
Unable to update the EntitySet 'Todos' because it has a DefiningQuery and no <InsertFunction> element exists in the <ModificationFunctionMapping> element to support the current operation.  
__Solution__  
Make sure a primary key is defined in your table as well as your model.  
A primary key is not the same as an Identity column. Primary key represents the unique column while Identity is for auto-increment.   

## Chapter 2: Entity Framework DB First - Managing Entity Relationships
__One-to-Many relationship__  
To implement a one-tomany relationship, we have to create a foreign key in the table that is on the _many_ end.

__One-to-One relationship__  
Entity Framework does not support having a foreign key in one of the related table for one-to-one relationship. One-to-one relationship is formed by making both the tables involved in the relationship share a primary key.

__Many-to-Many relationship__  
To implement a many-to-many relationship w, we need to create a third table. This table is often called a _join table_ or a _junction table_. The primary keys of this table consist of the foreign keys from both tables involved in the relationsip.  
