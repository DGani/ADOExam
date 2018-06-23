
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/22/2018 01:45:16
-- Generated from EDMX file: C:\Users\dgani\documents\visual studio 2015\Projects\ADONetExam\ADONetExam\Vacancy.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VacanciesDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Vacancies'
CREATE TABLE [dbo].[Vacancies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameVacancy] nchar(255)  NOT NULL,
    [URLVacancy] nchar(255)  NOT NULL,
    [DescriptionVacancy] nchar(255)  NOT NULL,
    [DatePublicationVacancy] datetime  NOT NULL,
    [EmailAuthorVacancy] nchar(255)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Vacancies'
ALTER TABLE [dbo].[Vacancies]
ADD CONSTRAINT [PK_Vacancies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------