using Azure.Identity;
using DotnetEssentials.AspNet.Core.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DotnetEssentials.AspNet.Core.Extensions;

public static class IHostBuilderExtensions
{
	/// <summary>
	/// Configuration extension for your app settings
	/// </summary>
	/// <param name="loadSettingsFile">name of appsettings file, if not passed anything it will take <em>appsettings.{environment}.{your machine name}.json</em>em></param>
	/// <param name="settings">if nothing passed or if null, it will skip get value from keyvault. <see cref="KeyVaultSettings"/> </param>
	/// <returns></returns>
	public static IHostBuilder ConfigureAppSettings(this IHostBuilder builder, string loadSettingsFile = "", KeyVaultSettings? settings = null) =>
		builder.ConfigureAppConfiguration((context, builder) =>
		{
			loadSettingsFile = string.IsNullOrWhiteSpace(loadSettingsFile) ? $"appsettings.{context.HostingEnvironment.EnvironmentName}.{Environment.MachineName}.json" : loadSettingsFile;
			var localBuilder = new ConfigurationBuilder();
			foreach (var src in builder.Sources)
			{
				localBuilder.Add(src);
			}
			localBuilder.AddJsonFile(loadSettingsFile, true);

			var localConfig = localBuilder.Build();

			if (settings != null)
			{
				var keyVaultSettings = localConfig.GetSection<KeyVaultSettings>(settings.KeyVaultSectionName);
				if (keyVaultSettings.Enabled)
				{
					builder.AddAzureKeyVault(new Uri(keyVaultSettings.Url), new DefaultAzureCredential());
				}
			}

			builder.AddJsonFile(loadSettingsFile, true);
		});
}