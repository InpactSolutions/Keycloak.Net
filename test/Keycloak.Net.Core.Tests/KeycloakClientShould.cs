using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Keycloak.Net.Tests
{
	public partial class KeycloakClientShould
    {
        private readonly KeycloakClient _client;

        public KeycloakClientShould()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
														  .AddJsonFile("appsettings.json",
																	   optional: true,
																	   reloadOnChange: true)
														  .Build();

            var url = configuration["url"]!;
            var userName = configuration["userName"]!;
            var password = configuration["password"]!;

            _client = new(url, userName, password);
        }

        private static readonly Lazy<HashSet<string>> _enabledFeatures = new(() =>
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                                          .Build();
            var client = new KeycloakClient(configuration["url"]!, configuration["userName"]!, configuration["password"]!);
            var info = client.GetServerInfoAsync("master").GetAwaiter().GetResult();
            return new HashSet<string>(
                info.Features?.Where(f => f.Enabled).Select(f => f.Name) ?? Enumerable.Empty<string>(),
                StringComparer.OrdinalIgnoreCase);
        });

        internal static bool IsServerFeatureEnabled(string featureName) => _enabledFeatures.Value.Contains(featureName);
    }
}
