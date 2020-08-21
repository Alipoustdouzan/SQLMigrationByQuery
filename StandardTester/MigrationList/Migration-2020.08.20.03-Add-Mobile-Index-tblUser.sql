--@strMigrationDesc=Add strMobile unique index on tblUser
IF NOT EXISTS (
              SELECT
                  *
              FROM sys.indexes
              WHERE name='IX_tblUser'
                    AND object_id=OBJECT_ID('tblUser')
              )
BEGIN

    CREATE UNIQUE NONCLUSTERED INDEX [IX_tblUser]
    ON [dbo].[tblUser]([strMobile] ASC)
    WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)
    ON [PRIMARY]
END
--EXECUTE ('select * from tblusdwer')

