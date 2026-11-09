using DougKlassen.Pegboard.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DougKlassen.Pegboard.Repositories
{
    internal class ObjectStylesJsonRepo : IObjectStylesRepo
    {
        private string filePath;

        public ObjectStylesJsonRepo(string jsonRepoFilePath)
        {
            filePath = jsonRepoFilePath;
        }

        public IEnumerable<ObjectStylesModel> LoadObjectStyles()
        {
            string jsonData = File.ReadAllText(filePath);
            try
            {
                List<ObjectStylesModel> objectStyles = (List<ObjectStylesModel>)JsonConvert.DeserializeObject(jsonData, typeof(List<ObjectStylesModel>));
                return objectStyles;
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't read Object Styles catalog", e);
            }
        }

        public void WriteObjectStyles(IEnumerable<ObjectStylesModel> catalog)
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
                throw new Exception("Couldn't write Object Styles catalog", e);
            }
        }
    }
}
