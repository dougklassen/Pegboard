using DougKlassen.Pegboard.Models;
using System.Collections.Generic;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IParameterCatalogRepo
    {
        IEnumerable<ParameterModel> LoadParameterCatalog();
        void WriteParameterCatalog(IEnumerable<ParameterModel> catalog);
    }
}
