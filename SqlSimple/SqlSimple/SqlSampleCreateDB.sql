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
