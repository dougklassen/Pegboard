using DougKlassen.Pegboard.Models;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IParameterCatalogRepo
    {
        IEnumerable<ParameterModel> LoadParameterCatalog();
        void WriteParameterCatalog(IEnumerable<ParameterModel> catalog);
    }
}
