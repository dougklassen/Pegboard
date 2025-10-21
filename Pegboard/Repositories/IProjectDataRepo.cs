using DougKlassen.Pegboard.Models;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IProjectDataRepo
    {
        ProjectDataModel LoadProjectDataCatalog();
        void WriteProjectDataCatalog(ProjectDataModel catalog);
    }
}
