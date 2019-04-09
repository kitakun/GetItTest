ECHO For initializating dotnet required!
cmd /c dotnet build %~dp0Voronov.GetItTestApp.Web\Voronov.GetItTestApp.Web.csproj
cmd /c dotnet ef database update --startup-project %~dp0Voronov.GetItTestApp.Web --project %~dp0Voronov.GetItTestApp.Persistnce.EntityFramework -c EntityFrameworkContext
pause