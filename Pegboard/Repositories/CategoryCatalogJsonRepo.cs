using DougKlassen.Pegboard.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DougKlassen.Pegboard.Repositories
{
    internal class CategoryCatalogJsonRepo : ICategoryCatalogRepo
    {
        private string filePath;

        public CategoryCatalogJsonRepo(string jsonRepoFilePath)
        {
            filePath = jsonRepoFilePath;
        }

        public IEnumerable<CategoryModel> LoadCategoryCatalog()
        {
            throw new NotImplementedException();
        }

        public void WriteScheduleCatalog(IEnumerable<CategoryModel> catalog)
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
