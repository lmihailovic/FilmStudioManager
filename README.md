# Film Studio Manager

Web based management app for working with employees and projects.

![image](https://github.com/user-attachments/assets/7fa9acd3-013f-4d50-b3c9-cd20e83311a9)


## Requirements

- `docker compose`
> [!CAUTION]
> Linux users should have `~/.dotnet/tools` in their `$PATH`

## Getting started

1. Clone the repository `git clone https://github.com/lmihailovic/FilmStudioManager`
2. Enter the repository `cd FilmStudioManager`
3. Start the database container `docker-compose up -d`
4. Apply the migrations `dotnet ef database update`
5. Start the project `dotnet run`
