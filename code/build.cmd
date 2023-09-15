@echo off

set solution=Enumerations.sln

dotnet build %solution% -c Debug
dotnet build %solution% -c Release
