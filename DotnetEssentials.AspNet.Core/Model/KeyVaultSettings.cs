namespace DotnetEssentials.AspNet.Core.Model;

public class KeyVaultSettings
{
	/// <summary>
	/// Keyvault Section name in configuration json file
	/// </summary>
	public string KeyVaultSectionName { get; set; } = "KeyVault";

	public string Url { get; set; }
	public bool Enabled { get; set; }
}