@echo off
echo Starting Backend...
cd Backend
dotnet tool restore
:: Эта команда создаст базу и применит миграции сама при запуске
dotnet ef database update
dotnet run
pause