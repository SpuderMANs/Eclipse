namespace Eclipse.Loader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;
    using Eclipse.API.Interfaces;

    public static class ConfigManager
    {
        private static readonly string ConfigFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Eclipse",
            "Configs",
            "config.yaml");

        private static readonly ISerializer Serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        private static readonly IDeserializer Deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        public static Dictionary<string, IConfig> Load(IEnumerable<IPlugin> plugins)
        {
            if (!File.Exists(ConfigFilePath))
                File.WriteAllText(ConfigFilePath, string.Empty);

            string rawYaml = File.ReadAllText(ConfigFilePath);

            var rawConfigs = string.IsNullOrWhiteSpace(rawYaml)
                ? new Dictionary<string, object>()
                : Deserializer.Deserialize<Dictionary<string, object>>(rawYaml);

            var loadedConfigs = new Dictionary<string, IConfig>();

            foreach (var plugin in plugins)
            {
                if (!rawConfigs.TryGetValue(plugin.Name, out object pluginData))
                {
                    loadedConfigs[plugin.Name] = plugin.Config;
                }
                else
                {
                    try
                    {
                        IConfig config = (IConfig)Deserializer.Deserialize(
                            Serializer.Serialize(pluginData),
                            plugin.Config.GetType());

                        plugin.Config = config;
                        loadedConfigs[plugin.Name] = config;
                    }
                    catch (Exception)
                    {
                        loadedConfigs[plugin.Name] = plugin.Config;
                    }
                }
            }

            return loadedConfigs;
        }

        public static void Save(IEnumerable<IPlugin> plugins)
        {
            var dictToSave = new Dictionary<string, IConfig>();
            foreach (var plugin in plugins)
            {
                dictToSave[plugin.Name] = plugin.Config;
            }

            string yaml = Serializer.Serialize(dictToSave);
            File.WriteAllText(ConfigFilePath, yaml);
        }

        public static void Reload(IEnumerable<IPlugin> plugins)
        {
            Load(plugins);
            Save(plugins);
        }
    }
}
