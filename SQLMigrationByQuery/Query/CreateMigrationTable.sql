﻿IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE TABLE_NAME='___DatabaseMigration'
              )
BEGIN

    CREATE TABLE [dbo].[___DatabaseMigration] (
                                              [intMigrationID] [BIGINT] NOT NULL IDENTITY(1, 1),
                                              [strMigrationProject] [VARCHAR](50) COLLATE Persian_100_CI_AS NOT NULL,
                                              [strMigrationName] [VARCHAR](50) COLLATE Persian_100_CI_AS NOT NULL,
                                              [strMigrationDesc] [NVARCHAR](MAX) COLLATE Persian_100_CI_AS NOT NULL,
                                              [datMigrationApply] [DATETIME] NOT NULL,
                                              [blnMigrationSuccess] [BIT] NOT NULL
                                              )
    ALTER TABLE [dbo].[___DatabaseMigration]
    ADD
        CONSTRAINT [PK____DatabaseMigration] PRIMARY KEY CLUSTERED([intMigrationID])

   CREATE UNIQUE NONCLUSTERED INDEX [IX____DatabaseMigration] ON [dbo].[___DatabaseMigration]
(
	[strMigrationProject] ASC,
	[strMigrationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END