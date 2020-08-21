--@strMigrationDesc=Add address and mobile in tblUser
IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.COLUMNS
              WHERE TABLE_NAME='tblUser'
                    AND COLUMN_NAME='strMobile'
              )
BEGIN
    ALTER TABLE dbo.tblUser ADD [strMobile] VARCHAR(11) NOT NULL
END
IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.COLUMNS
              WHERE TABLE_NAME='tblUser'
                    AND COLUMN_NAME='strAddress'
              )
BEGIN
    ALTER TABLE dbo.tblUser ADD [strAddress] NVARCHAR(300) NOT NULL
END