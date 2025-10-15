using DougKlassen.Pegboard.Models;
using Newtonsoft.Json;
using System.IO;

namespace DougKlassen.Pegboard.Repositories
{
    internal class ScheduleCatalogJsonRepo : IScheduleCatalogRepo
    {
        private String filePath;

        public ScheduleCatalogJsonRepo(String jsonRepoFilePath)
        {
            filePath = jsonRepoFilePath;
        }

        public IEnumerable<ScheduleModel> LoadScheduleCatalog()
        {
            throw new NotImplementedException();
        }

        public void WriteScheduleCatalog(IEnumerable<ScheduleModel> catalog)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            String jsonData = JsonConvert.SerializeObject(catalog, settings);
            try
            {
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't write Schedule catalog", e);
            }
        }
    }
}
