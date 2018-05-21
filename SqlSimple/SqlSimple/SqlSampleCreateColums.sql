USE [SqlSample];
GO

exec sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE';
GO
PRINT 'User colums are creating'
ALTER TABLE	[User]	

	Add  
    FullName nvarchar(25) NOT NULL,  
    Email nvarchar(30);
GO

print 'User Colums are created'

PRINT '[House] Table Created.'

PRINT 'House colums are creating'
ALTER TABLE	[House]

       ADD	
            Address nvarchar(25) NOT NULL,
			UserId int 
			IDENTITY(1,1) PRIMARY KEY (Id),
			CONSTRAINT FK_UserHouse FOREIGN KEY (UserId)
			REFERENCES [User](Id);
GO
print 'House Colums are created'
