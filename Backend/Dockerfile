# Этап 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
# Если твой файл называется иначе, замени Backend.csproj на свое имя
COPY *.csproj ./
RUN dotnet restore

# Копируем все остальные файлы и собираем Release-версию
COPY . ./
RUN dotnet publish -c Release -o out

# Этап 2: Запуск готового приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копируем собранные файлы из предыдущего этапа
COPY --from=build /app/out .

# Render требует, чтобы приложение слушало порт 10000
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Заменяем Backend.dll на имя твоего основного файла проекта
ENTRYPOINT ["dotnet", "Backend.dll"]