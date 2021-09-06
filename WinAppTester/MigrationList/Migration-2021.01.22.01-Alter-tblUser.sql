--@strMigrationDesc=Add strMobile unique index on tblUser
ALTER TABLE dbo.tblUser ADD [strMotherName] NVARCHAR(50) NOT NULL
Go
ALTER TABLE dbo.tblUser ADD [strFatherName] NVARCHAR(50) NOT NULL
GO
