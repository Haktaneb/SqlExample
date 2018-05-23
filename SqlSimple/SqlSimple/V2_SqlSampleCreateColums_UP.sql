USE [SqlSample];
GO

exec sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE';
GO

PRINT '#############'
PRINT 'v2_'
PRINT '#############'

PRINT 'Creating User Table.'
CREATE TABLE [User]  

   (Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    FullName nvarchar(25) NOT NULL,  
    Email nvarchar(30));

GO

PRINT 'User Table Created.'

PRINT 'Creating [House] Table'

CREATE Table [House]
(
  
  
    Id int IDENTITY(1,1) PRIMARY KEY (Id) NOT NULL,
            Address nvarchar(25) NOT NULL,
			UserId int ,
			CONSTRAINT FK_UserHouse FOREIGN KEY (UserId)
			REFERENCES [User](Id)
   );
GO

PRINT '[House] Table Created.'

PRINT 'User colums are creating'
ALTER TABLE	[User]	

	Add  Phone nvarchar(13)
    
GO

print 'User Colums  created'


