USE [SqlSample];
GO

exec sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE';
GO

PRINT 'Creating User Table.'
CREATE TABLE [User]  

   (Id int IDENTITY(1,1) PRIMARY KEY NOT NULL);

GO

PRINT 'User Table Created.'

PRINT 'Creating [House] Table'

CREATE Table [House]
(
  
  
   Id int  NOT NULL

   );
GO

PRINT '[House] Table Created.'