USE master
GO

IF db_id('[SqlSample]') IS NOT NULL BEGIN

	PRINT 'Dropping database...'

	ALTER DATABASE [SqlSample]
	SET SINGLE_USER
	WITH ROLLBACK IMMEDIATE;

	DROP DATABASE [SqlSample];

	PRINT 'Dropped database.'

END ELSE BEGIN

	PRINT 'Database does not exist. Not dropping it :)'

END

PRINT 'Creating database...'
CREATE DATABASE [SqlSample]
PRINT 'Created database.'
GO

USE [SqlSample];
GO

exec sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE';
GO

PRINT 'Creating User Table.'
CREATE TABLE [User]  
   (Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,  
    FullName nvarchar(25) NOT NULL,  
    Email nvarchar(30))  
GO

PRINT 'User Table Created.'

PRINT 'Creating [House] Table'

CREATE Table [House]
(
  
   Id int IDENTITY(1,1) PRIMARY KEY NOT NULL, 
   Address nvarchar(25) NOT NULL

   )
GO

PRINT '[House] Table Created.'