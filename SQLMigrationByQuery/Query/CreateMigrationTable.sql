﻿IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE TABLE_NAME='___DatabaseMigration'
              )
BEGIN

    CREATE TABLE [dbo].[___DatabaseMigration] (
                                              [intMigrationID] [BIGINT] NOT NULL IDENTITY(1, 1),
                                              [strMigrationProject] [VARCHAR](200) NOT NULL,
                                              [strMigrationName] [VARCHAR](300) NOT NULL,
                                              [strMigrationDesc] [NVARCHAR](MAX) NOT NULL,
                                              [datMigrationApply] [DATETIME] NOT NULL,
                                              [blnMigrationSuccess] [BIT] NOT NULL
                                              )
    ALTER TABLE [dbo].[___DatabaseMigration]
    ADD
        CONSTRAINT [PK____DatabaseMigration] PRIMARY KEY CLUSTERED([intMigrationID])

    CREATE UNIQUE NONCLUSTERED INDEX [IX____DatabaseMigration]
    ON [dbo].[___DatabaseMigration]([strMigrationProject] ASC, [strMigrationName] ASC)
    WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)
    ON [PRIMARY]
END
ELSE
BEGIN
    IF(
      EXISTS (
             SELECT
                 *
             FROM INFORMATION_SCHEMA.COLUMNS
             WHERE TABLE_NAME='___DatabaseMigration'
                   AND COLUMN_NAME='strMigrationProject'
                   AND CHARACTER_MAXIMUM_LENGTH<200
             )
      OR EXISTS (
                SELECT
                    *
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME='___DatabaseMigration'
                      AND COLUMN_NAME='strMigrationName'
                      AND CHARACTER_MAXIMUM_LENGTH<300
                )
      )
    BEGIN
        DROP INDEX [IX____DatabaseMigration] ON [dbo].[___DatabaseMigration]
        ALTER TABLE dbo.___DatabaseMigration
        ALTER COLUMN strMigrationProject [VARCHAR](200)  NOT NULL
        ALTER TABLE dbo.___DatabaseMigration
        ALTER COLUMN strMigrationName [VARCHAR](300)  NOT NULL
        CREATE UNIQUE NONCLUSTERED INDEX [IX____DatabaseMigration]
        ON [dbo].[___DatabaseMigration]([strMigrationProject] ASC, [strMigrationName] ASC)
        WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)
        ON [PRIMARY]
    END
END