
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/15/2021 23:54:06
-- Generated from EDMX file: C:\Users\bbdnet2119\source\repos\Entity-Framework\Mastering-Entity-Framework\chp1\ModelFirst\ModelFirst\ModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModelFirst];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Todoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Todoes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Todoes'
CREATE TABLE [dbo].[Todoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TodoItem] nvarchar(max)  NOT NULL,
    [IsDone] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Todoes'
ALTER TABLE [dbo].[Todoes]
ADD CONSTRAINT [PK_Todoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------