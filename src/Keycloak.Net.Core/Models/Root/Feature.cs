namespace Keycloak.Net.Models.Root;

public class Feature
{
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("label")]
	public string Label { get; set; }

	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("dependencies")]
	public List<string> Dependencies { get; set; }

	[JsonPropertyName("enabled")]
	public bool Enabled { get; set; }
}
