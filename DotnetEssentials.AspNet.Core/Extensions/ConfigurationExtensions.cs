using Microsoft.Extensions.Configuration;

namespace DotnetEssentials.AspNet.Core.Extensions;

public static class ConfigurationExtensions
{
	public static T GetSection<T>(this IConfiguration configuration, string key) => configuration.GetSection(key).Get<T>();

	public static T GetSection<T>(this IConfiguration configuration) => configuration.GetSection(typeof(T).Name).Get<T>();
}