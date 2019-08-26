-- Create Service User

DECLARE @SqlStatement NVARCHAR(500)

IF NOT EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = '$(ServiceUser)' AND DBNAME = '$(DatabaseName)')
BEGIN
    SET @SqlStatement = 'CREATE LOGIN ' + QUOTENAME('$(ServiceUser)') + ' FROM WINDOWS WITH DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english]'
    EXEC sp_executesql @SqlStatement
END

IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = '$(ServiceUser)')
BEGIN
    CREATE USER [$(ServiceUser)] FOR LOGIN [$(ServiceUser)] WITH DEFAULT_SCHEMA = GameDeals
END

GRANT CONNECT TO [$(ServiceUser)]
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::GameDeals TO [$(ServiceUser)]


-- Create App Pool User

IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = '$(WebAppUser)')
BEGIN
    CREATE USER [$(WebAppUser)] FOR LOGIN [$(WebAppUser)] WITH DEFAULT_SCHEMA = GameDeals
END

GRANT CONNECT TO [$(WebAppUser)]
GRANT SELECT ON SCHEMA::GameDeals TO [$(WebAppUser)]
GRANT UPDATE ON GameDeals.Posts TO [$(WebAppUser)]
