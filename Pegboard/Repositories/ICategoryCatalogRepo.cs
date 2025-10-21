using DougKlassen.Pegboard.Models;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface ICategoryCatalogRepo
    {
        IEnumerable<CategoryModel> LoadCategoryCatalog();
        void WriteScheduleCatalog(IEnumerable<CategoryModel> catalog);
    }
}
