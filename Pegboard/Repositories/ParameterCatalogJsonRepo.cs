using DougKlassen.Pegboard.Models;
using Newtonsoft.Json;
using System.IO;

namespace DougKlassen.Pegboard.Repositories
{
    internal class ParameterCatalogJsonRepo : IParameterCatalogRepo
    {
        private String filePath;

        public ParameterCatalogJsonRepo(String jsonRepoFilePath)
        {
            filePath = jsonRepoFilePath;
        }

        public IEnumerable<ParameterModel> LoadParameterCatalog()
        {
            throw new NotImplementedException();
        }

        public void WriteParameterCatalog(IEnumerable<ParameterModel> catalog)
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
                throw new Exception("Couldn't write Parameters catalog", e);
            }
        }
    }
}
