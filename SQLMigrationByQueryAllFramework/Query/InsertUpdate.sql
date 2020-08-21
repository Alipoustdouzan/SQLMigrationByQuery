IF EXISTS (
          SELECT
              *
          FROM dbo.___DatabaseMigration
          WHERE strMigrationProject=@strMigrationProject
                AND strMigrationName=@strMigrationName
          )
BEGIN
    UPDATE
        dbo.___DatabaseMigration
    SET
        strMigrationDesc=@strMigrationDesc,
        datMigrationApply=@datMigrationApply,
        blnMigrationSuccess=@blnMigrationSuccess
    WHERE strMigrationProject=@strMigrationProject
          AND strMigrationName=@strMigrationName
END
ELSE
BEGIN
    INSERT dbo.___DatabaseMigration
    (
    strMigrationProject,
    strMigrationName,
    strMigrationDesc,
    datMigrationApply,
    blnMigrationSuccess
    )
    VALUES
    (
    @strMigrationProject,
    @strMigrationName,
    @strMigrationDesc,
    @datMigrationApply,
    @blnMigrationSuccess
    )
END