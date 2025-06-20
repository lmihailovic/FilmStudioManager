# Film Studio Manager

Web based management app for working with employees and projects.

![image](https://github.com/user-attachments/assets/7fa9acd3-013f-4d50-b3c9-cd20e83311a9)


## Requirements

- `docker compose`
> [!CAUTION]
> Linux users should have `~/.dotnet/tools` in their `$PATH`

## Getting started

Clone and enter the repository
```
git clone https://github.com/lmihailovic/FilmStudioManager
&& cd FilmStudioManager
```
Start the database container
```
docker-compose up -d
```
Apply the migrations and start the project
```
dotnet ef database update
&& dotnet run
```
