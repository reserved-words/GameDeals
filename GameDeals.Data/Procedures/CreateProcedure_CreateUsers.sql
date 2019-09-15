IF EXISTS (select * from sys.objects o inner join sys.schemas s on s.schema_id = o.schema_id where s.name = 'GameDeals' and o.name = 'CreateUsers' and o.type = 'P') 
BEGIN
	DROP PROCEDURE [GameDeals].[CreateUsers]
END
GO

CREATE PROCEDURE [GameDeals].[CreateUsers]
	@DatabaseName NVARCHAR(100),
	@ServiceUser NVARCHAR(100),
	@WebAppUser NVARCHAR(100)
AS
BEGIN
	DECLARE @SqlStatement NVARCHAR(500)


	-- Create Service User

	IF NOT EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = @ServiceUser AND DBNAME = @DatabaseName)
	BEGIN
		SET @SqlStatement = 'CREATE LOGIN [' + @ServiceUser + '] FROM WINDOWS WITH DEFAULT_DATABASE=[' + @DatabaseName + '], DEFAULT_LANGUAGE=[us_english]'
		EXEC sp_executesql @SqlStatement
	END

	IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = @ServiceUser)
	BEGIN
		SET @SqlStatement = 'CREATE USER [' + @ServiceUser + '] FOR LOGIN [' + @ServiceUser + '] WITH DEFAULT_SCHEMA = GameDeals'
		EXEC sp_executesql @SqlStatement
	END

	SET @SqlStatement = 'GRANT CONNECT TO [' + @ServiceUser + ']'
	EXEC sp_executesql @SqlStatement

	SET @SqlStatement = 'GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::GameDeals TO [' + @ServiceUser + ']'
	EXEC sp_executesql @SqlStatement


	-- Create App Pool User

	IF NOT EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = @WebAppUser AND DBNAME = @DatabaseName)
	BEGIN
		SET @SqlStatement = 'CREATE LOGIN [' + @WebAppUser + '] FROM WINDOWS WITH DEFAULT_DATABASE=[' + @DatabaseName + '], DEFAULT_LANGUAGE=[us_english]'
		EXEC sp_executesql @SqlStatement
	END

	IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = @WebAppUser)
	BEGIN
		SET @SqlStatement = 'CREATE USER [' + @WebAppUser + '] FOR LOGIN ' + @WebAppUser + ' WITH DEFAULT_SCHEMA = GameDeals'
		EXEC sp_executesql @SqlStatement
	END

	SET @SqlStatement = 'GRANT CONNECT TO [' + @WebAppUser + ']'
	EXEC sp_executesql @SqlStatement

	SET @SqlStatement = 'GRANT SELECT ON SCHEMA::GameDeals TO [' + @WebAppUser + ']'
	EXEC sp_executesql @SqlStatement

	SET @SqlStatement = 'GRANT UPDATE ON GameDeals.Posts TO [' + @WebAppUser + ']'
	EXEC sp_executesql @SqlStatement
END
GO