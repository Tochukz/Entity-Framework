 IF NOT EXISTS (SELECT * FROM sys.databases WHERE NAME = 'Chp2_DBFirst')
  BEGIN 
	CREATE DATABASE Chp2_DBFirst;
  END
  GO

USE Chp2_DBFirst;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE NAME='Employers' AND xtype='U') 
  BEGIN 
     CREATE TABLE Employers(
	   Id INT PRIMARY KEY IDENTITY (1, 1),
	   EmployerName VARCHAR(100),
	 )
  END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE NAME='Employees' AND xtype = 'U')
  BEGIN 
    CREATE TABLE Employees(
	  Id INT PRIMARY KEY IDENTITY (1, 1), 
	  EmployeeName VARCHAR(60),
	  EmployerId INT NOT NULL,

	  CONSTRAINT FK_Employees_Employers FOREIGN KEY (EmployerId) 
	  REFERENCES Employers (Id) 
	  ON DELETE CASCADE 
	  ON UPDATE CASCADE 
	)
  END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE NAME='Passports')
  BEGIN 
    CREATE TABLE Passports (
      Id INT PRIMARY KEY,
	  PassportNumber INT NOT NULL,

	  CONSTRAINT FK_Passport_Employees FOREIGN KEY (Id)
	  REFERENCES Employees (Id)
	  ON UPDATE CASCADE
	  ON DELETE CASCADE 	   
	)
  END 

IF NOT EXISTS (SELECT * FROM sysobjects WHERE NAME = 'Reports')
  BEGIN 
    CREATE TABLE Reports (
	  Id INT PRIMARY KEY IDENTITY(1, 1),
	  ReportName VARCHAR(60),
	  ReportFilePath VARCHAR(120),
	)
  END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE NAME = 'EmployeeReports')
  BEGIN 
    CREATE TABLE EmployeeReports(
	  EmployeeId INT NOT NULL, 
	  ReportId INT NOT NULL, 

	  CONSTRAINT  FK_EmployeeReports_Employee FOREIGN KEY (EmployeeId) 
	  REFERENCES Employees (Id)
	  ON UPDATE CASCADE,
	
      CONSTRAINT FK_EmployeeReports_Report FOREIGN KEY (ReportId)
	  REFERENCES Reports (Id)
	  ON UPDATE CASCADE
	) 
  END