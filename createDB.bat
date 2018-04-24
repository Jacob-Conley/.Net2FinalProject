
echo off

rem batch file to run a script to create a db
rem 11/5/2017

sqlcmd -S localhost -E -i gameDB.sql

rem sqlcmd -S localhost/sqlexpress -E -i gameDB.sql
rem sqlcmd -S localhost/mssqlserver -E -i gameDB.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE