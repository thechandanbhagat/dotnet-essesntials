using DotnetEssentials.AzureFunction.Attributes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotnetEssentials.AzureFunction.Extension
{
    public static class ConfigurationExtension
    {
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
}