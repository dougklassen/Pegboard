using DougKlassen.Pegboard.Models;
using Newtonsoft.Json;
using System.IO;

namespace DougKlassen.Pegboard.Repositories
{
    internal class VisibilitySettingsJsonRepo : IVisibilitySettingsRepo
    {
        private static string configFileName = "VizSettings.json";
        private static string configFilePath = FileLocations.AddInDirectory + configFileName;

        public VisibilitySettings LoadSettings()
        {
            VisibilitySettings settings;

            if (!File.Exists(configFilePath))
            {
                WriteSettings(new VisibilitySettings());
            }

            try
            {
                string jsonText = File.ReadAllText(configFilePath);
                settings = JsonConvert.DeserializeObject<VisibilitySettings>(jsonText);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't read config file", e);
            }

            return settings;
        }

        public void WriteSettings(VisibilitySettings settings)
        {
            if (!Directory.Exists(FileLocations.AddInDirectory))
            {
                Directory.CreateDirectory(FileLocations.AddInDirectory);
            }

            try
            {
                string jsonText = JsonConvert.SerializeObject(settings);
                File.WriteAllText(configFilePath, jsonText);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't write config file", e);
            }
        }
    }
}
