--@strMigrationDesc=Create tblUser
IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE TABLE_NAME='tblUser'
              )
BEGIN

    CREATE TABLE [dbo].[tblUser] (
                                 [intUserID] [BIGINT] IDENTITY(1, 1) NOT NULL,
                                 [strUser] [NVARCHAR](MAX) NOT NULL,
                                 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED([intUserID] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY]
                                 )ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    ALTER TABLE [dbo].[tblUser] WITH CHECK
    ADD
        CONSTRAINT [FK_tblUser_tblUser] FOREIGN KEY([intUserID])REFERENCES [dbo].[tblUser]([intUserID])
    ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblUser]
END