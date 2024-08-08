using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace MiNa.InventorAddInDebugger
{
    /// <summary>
    /// Provides functionality for load and save configuration
    /// </summary>
    public class AddInLoaderConfigManager
    {
        /// <summary>
        /// Gets the config file name
        /// </summary>
        public string ConfigFileName => Path.Combine(
            ConfigFileFolder,
            "InventorAddInDebugger.config");

        string ConfigFileFolder { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "InventorAddInDebugger");

        /// <summary>
        /// Loads the configuration from local file
        /// </summary>
        /// <param name="config">Target config to load</param>
        public void Load(AddInLoaderConfig config)
        {
            var load = LoadFromFile();
            if (config == null) config = new AddInLoaderConfig();

            foreach (var propertyInfo in load.GetType().GetProperties())
            {
                if (!(propertyInfo.CanRead && propertyInfo.CanWrite))
                    continue;

                config.GetType().InvokeMember(propertyInfo.Name,
                    BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance, null, config, new[]
                    {
                        load.GetType().InvokeMember(propertyInfo.Name,
                            BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, load, null)
                    });
            }
        }

        /// <summary>
        /// Saves the configuration to file
        /// </summary>
        /// <param name="config">Config to save</param>
        public void Save(AddInLoaderConfig config)
        {
            var xmlSerializer = new XmlSerializer(config.GetType());
            var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, config);

            if (!Directory.Exists(ConfigFileFolder))
                Directory.CreateDirectory(ConfigFileFolder);

            File.WriteAllText(ConfigFileName, textWriter.ToString());
        }

        private AddInLoaderConfig LoadFromFile()
        {
            if (!File.Exists(ConfigFileName))
            {
                var config = new AddInLoaderConfig();
                Save(config);
                return config;
            }

            try
            {
                var readAllText = File.ReadAllText(ConfigFileName);
                var xmlSerializer = new XmlSerializer(typeof(AddInLoaderConfig));
                var textReader = new StringReader(readAllText);
                var deserialize = xmlSerializer.Deserialize(textReader);
                return deserialize as AddInLoaderConfig;
            }
            catch (Exception ex)
            {
                var config = new AddInLoaderConfig();
                return config;
            }
        }
    }
}