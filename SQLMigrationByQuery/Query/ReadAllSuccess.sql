SELECT
    *
FROM dbo.___DatabaseMigration
WHERE blnMigrationSuccess = 1
    AND strMigrationProject=@strMigrationProject