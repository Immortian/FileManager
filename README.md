# FileManager

My first MVVM application on WPF.

## Whats Including In This Repository

Besides WPF .NET application this repository contains a docker compose file with postgre:13.3 and pgadmin4 containers that will help you launch the project

## Run The Project
You will need the following tools:

* [Visual Studio](https://visualstudio.microsoft.com/en/)
* [PostgreSql](https://www.postgresql.org/download/windows/) (optional)
* [Git Bash](https://git-scm.com/downloads)

If you have no PosgreSql you can use:

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Configuration
Follow these steps to get your development environment set up:

1. Clone this repository:
```
 git clone "https://github.com/Immortian/FileManager"
```
2. (If you work with your own PostgreSql) Go to App.xaml.cs to modify db connection string
3. (If you work with Docker Desktop) Open cmd and change directory to FileManager than use:
```
docker compose up -d
```
4. Open your DBMS (pgadmin4 will run on http://localhost:5050/) and connect to your db server (if you use containers, all params you can see in docker-compose.yml)
5. Restore packages and build up project

Application is ready to start. If you want permission to see your system files run it as administrator.

## Explanation of nuances

1. EnviromentProvider will automaticly ignor all folders with attribute ReparsePoint and System
2. File icons are just png and there are only 7 icons for: folders, .doc/.docx, .xls/.xlsx/.xml, .pdf, .png/.jpg/.jpeg, .url for links and one ('unknown') for everything else
3. There are some kind of dependency injection in app.xaml.cs
4. There are DbInitializer class ensures that database exists or will be automaticaly created from model (there are also CreateDatabase.sql if you interested in sql code)
