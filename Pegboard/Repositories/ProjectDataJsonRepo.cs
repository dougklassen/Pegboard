using DougKlassen.Pegboard.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DougKlassen.Pegboard.Repositories
{
    internal class ProjectDataJsonRepo : IProjectDataRepo
    {
        private string filePath;

        public ProjectDataJsonRepo(string jsonRepoFilePath)
        {
            filePath = jsonRepoFilePath;
        }

        public ProjectDataModel LoadProjectDataCatalog()
        {
            throw new NotImplementedException();
        }

        public void WriteProjectDataCatalog(ProjectDataModel catalog)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            string jsonData = JsonConvert.SerializeObject(catalog, settings);
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
