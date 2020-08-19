# SQLMigrationByQuery
Execute SQL migration query list in .net projects

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
# Tips
1. If migration query execute successfully it means that query will never execute again.
2. Migration .sql files will execute by STRING order so make sure their names are ok.
3. If any query fail the process will stop and return FALSE.
4. You can't use GO in your .sql file, If you need it you can add new .sql file.
5. All command in same .sql file will execute in a transaction, It will rollback if query fail.

# Author messege
I write this library for my personal use but i think it could be useful to other people which they don't want use Entity Framework code first.

If you think any place need an improvement please let me know by pull reuqest.
