using Newtonsoft.Json;
using System.IO;

namespace DougKlassen.Pegboard.Models
{
    internal class VisibilitySettingsJsonRepo : IVisibilitySettingsRepo
    {
        private static String configFileName = "VizSettings.json";
        private static String configFilePath = FileLocations.AddInDirectory + configFileName;

        public VisibilitySettings LoadSettings()
        {
            VisibilitySettings settings;

            if (!File.Exists(configFilePath))
            {
                WriteSettings(new VisibilitySettings());
            }

            try
            {
                String jsonText = File.ReadAllText(configFilePath);
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
                String jsonText = JsonConvert.SerializeObject(settings);
                File.WriteAllText(configFilePath, jsonText);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't write config file", e);
            }
        }
    }
}
