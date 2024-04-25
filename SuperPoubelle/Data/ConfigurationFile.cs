using Newtonsoft.Json;
using System.IO;

namespace SuperPoubelle.Data
{
    internal class ConfigurationFile
    {
        private static ConfigurationFile _instance;

        public const string FileName = "configuration.json";

        private ConfigurationFile()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var file = Path.Combine(path, FileName);

            if (!File.Exists(file))
            {
                Config = new ConfigurationJson { ReadingDelaysInMilliseconds = 500, DifferenceForGarbageDecision = 1000 };
                var jsonString = JsonConvert.SerializeObject(Config, Formatting.Indented);
                File.WriteAllText(file, jsonString);
                return;
            }

            Config = JsonConvert.DeserializeObject<ConfigurationJson>(File.ReadAllText(file))!;

        }
        public ConfigurationJson Config { get; set; }
        public static ConfigurationFile Instance => _instance ??= new ConfigurationFile();

        public class ConfigurationJson
        {
            public int ReadingDelaysInMilliseconds { get; set; }
            public int DifferenceForGarbageDecision { get; set; }
        }
    }
}
