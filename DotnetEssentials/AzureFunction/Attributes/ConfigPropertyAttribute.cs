namespace DotnetEssentials.AzureFunction.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ConfigPropertyAttribute : Attribute
    {
        private string configName;
        public string ConfigName => configName;

        public ConfigPropertyAttribute(string configName)
        {
            this.configName = configName;
        }
    }
}