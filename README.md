# SQLMigrationByQuery
Execute SQL migration query list in .net projects. This library is useful for software developers who don't want to use something like Entinty Framework code first and they prefer write their migration query by themselves.

# Features
SQLMigrationByQuery is .net NuGet Package which control your database migration querys, This library will execute all .sql file which you mark them in you main project. After execute any query in your query list a log will save in database.

# How to use?
1. Add SQLMigrationByQuery NuGet Package to your project.
2. Create a .sql file in your project.
3. Set any 'Migration Mark' you want in .sql file name, For example "Migration-2020.08.18.sql", The word "Migration-" is 'Migration Mark'.
4. Call the fuction below in any place you want execute your migration querys.

C#
```
bool blnMigrationResult= SQLMigrationByQuery.clsMigration.getApplyMigration(strConnectionString, "Migration-");
```
VB
```
Dim blnMigrationResult as boolean = SQLMigrationByQuery.clsMigration.getApplyMigration(strConnectionString, "Migration-")
```
If this fuction TRUE it means all query all execute successfully. 

You can check the migration result in database by below query
```
SELECT
    *
FROM dbo.___DatabaseMigration
```

The .sql sample :
```
--@strMigrationDesc=Add address and mobile for user table
IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.COLUMNS
              WHERE TABLE_NAME='tblUser'
                    AND COLUMN_NAME='strMobile'
              )
BEGIN
    ALTER TABLE dbo.tblUser ADD [strMobile] VARCHAR(11) NULL
END
IF NOT EXISTS (
              SELECT
                  *
              FROM INFORMATION_SCHEMA.COLUMNS
              WHERE TABLE_NAME='tblUser'
                    AND COLUMN_NAME='strAddress'
              )
BEGIN
    ALTER TABLE dbo.tblUser ADD [strAddress] NVARCHAR(300) NULL
END
```

# Tips
1. If migration query execute successfully it means that query will never execute again.
2. Migration .sql files will execute by STRING order so make sure their names are ok.
3. If any query fail the process will stop and return FALSE.
4. You can't use GO in your .sql file, If you need it you can add new .sql file.
5. All command in same .sql file will execute in a transaction, It will rollback if query fail.
6. The .sql file name should be unique otherwise they second query with same name will never execute.
7. The migration description exists in your .sql files will be save in migration log (dbo.___DatabaseMigration table)
8. Make sure your .sql files build action is set to "Embedded Resource".

# Author message
I write this library for my personal use but i think it could be useful to other software developers which they don't want to use Entity Framework code first.

If you think any place need an improvement please let me know by pull reuqest.
