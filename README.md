# SQLMigrationByQuery
This library is SQL database schema version control, This library will execute SQL schema migration query list in .net projects. It's useful for software developers who don't want to use something like Entinty Framework code first and they prefer write their schema migration query by themselves.

# Features
SQLMigrationByQuery is .net NuGet Package which control your database schema migration querys, This library will execute all .sql file which you mark them in you main project. After execute any query in your query list a log will save in database.

# Version Log
ver 1.0.015 :
- Remove Remove Persian_100_CI_AS For Support SQL Server 2005

ver 1.0.0.14 :
- Add IDbConnection support in requestMigration class

ver 1.0.0.13 :
- Fix issue in upgrade old SQLMigrationByQuery packages (<1.0.0.6) to new packages

ver 1.0.0.12 :
- Add .NetStandard >=2 & .Net Core >=2 & .Net Framework >=4 support

ver 1.0.0.11 :
- Extand timeout to 600 sec

ver 1.0.0.10 :
- Extend MigrationName and MigrationProject string length
- Support GO in .sql file
- Support replace text in query at execution time

# How to use?
1. Add SQLMigrationByQuery NuGet Package to your project.
2. Create a .sql file in your project.
3. Make sure your .sql files build action is set to "Embedded Resource".
3. Set any 'MigrationMark' you want in .sql file name, For example "Migration-2020081801-FirstInit.sql", The word "Migration-" is 'MigrationMark'.
4. Call the fuction below in any place you want execute your migration query list.

You can always see the full sample of usage in WinAppTester project.


```C#
SQLMigrationByQuery.requestMigration objRequest = new SQLMigrationByQuery.requestMigration();
objRequest.ConnectionString = "YourConnectionString";
objRequest.CallerProjectName = "YourProjectName";
objRequest.MigrationMark = "Migration-";
objRequest.ReplaceTextInQuery = false;
objRequest.ReplaceTextSource = "";
objRequest.ReplaceTextTarget = "";
SQLMigrationByQuery.resultMigration objResult = SQLMigrationByQuery.helperMigration.getApplyMigration(objRequest);
if (objResult.Success == true)
{
    MessageBox.Show("Migration was successful");
}
else
{
    MessageBox.Show(objResult.ResultMessage);
}
```
If objResult.Success be TRUE it means all query all execute successfully, Otherwise you can find the error in objResult.ResultMessage.

You can check the migration result in database by below query
```SQL
SELECT * FROM dbo.___DatabaseMigration
```

The .sql sample :
```SQL
--@strMigrationDesc=Add address and mobile for user table
ALTER TABLE dbo.tblUser ADD [strMobile] VARCHAR(11) NULL
GO
ALTER TABLE dbo.tblUser ADD [strAddress] NVARCHAR(300) NULL
GO
```
Set your migration description front of --@strMigrationDesc= in your .sql file.

# Tips
1. If migration query execute successfully it means that query will never execute again.
2. Migration .sql files will execute by STRING order so make sure their names are ok.
3. If any query fail the process will stop and return FALSE and the error in ResultMessage property.
4. You can use GO in your .sql file now (version >= 1.0.0.10).
5. All command in same .sql file will execute in a transaction, It will rollback if of the batchs in query fail.
6. The .sql file name should be unique otherwise they second query with same name will never execute.
7. The migration description exists in your .sql files will be save in migration log (dbo.___DatabaseMigration table)
8. Make sure your .sql files build action is set to "Embedded Resource".
9. The caller project name ("YourProjectName") is useful when you want call migration library on more than one project on same database.
10. If you don't need replace text in your querys just ignore ReplaceTextInQuery, ReplaceTextSource and ReplaceTextTarget property and don't fill them.

# Warning
You shouldn't change CallerProjectName in future, Otherwise all query will execute again with new project name.
You shouldn't rename your .sql files otherwise that new .sql will execute again.

# Author message
I write this library for my personal use but i think it could be useful to other software developers which they don't want to use Entity Framework code first.

If you think any place need an improvement please let me know by pull reuqest.
