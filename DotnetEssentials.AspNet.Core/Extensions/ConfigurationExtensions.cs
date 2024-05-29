using DotnetEssentials.AspNet.Core.Attributes;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DotnetEssentials.AspNet.Core.Extensions;

public static class ConfigurationExtensions
{
    public static T GetSection<T>(this IConfiguration configuration, string key) => configuration.GetSection(key).Get<T>();

    public static T GetSection<T>(this IConfiguration configuration) => configuration.GetSection(typeof(T).Name).Get<T>();

    public static TModel LoadConfig<TModel>(this IConfiguration config)
            where TModel : class
    {
        var ret = Activator.CreateInstance<TModel>();
        var props = ret.GetType().GetProperties();
        foreach (var prop in props)
        {
            var attr = prop.GetCustomAttribute<ConfigPropertyAttribute>();
            if (attr != null)
            {
                prop.SetValue(ret, config[attr.ConfigName]);
            }
        }
        return ret;
    }
}