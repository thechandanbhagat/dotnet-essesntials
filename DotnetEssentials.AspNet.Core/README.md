
# Dotnet Essentials for ASP.NET Core application
---
## Extension
- Configuration Extension
  1. Create a file called ***appsettings.\{environment\}.\{your machine name\}.json*** file and add the necessary settings.
  2. in **Program.cs** file add **ConfigureAppSettings()** function as shown in the file
```csharp
    //just add .ConfigurationAppSettings(), if you want the default appsettings
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .ConfigureAppSettings();
```
- 
---