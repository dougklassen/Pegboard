using DougKlassen.Pegboard.Models;
using System.Collections.Generic;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface ICategoryCatalogRepo
    {
        IEnumerable<CategoryModel> LoadCategoryCatalog();
        void WriteScheduleCatalog(IEnumerable<CategoryModel> catalog);
    }
}
