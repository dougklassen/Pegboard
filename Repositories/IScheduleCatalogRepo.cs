using DougKlassen.Pegboard.Models;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IScheduleCatalogRepo
    {
        IEnumerable<ScheduleModel> LoadScheduleCatalog();
        void WriteScheduleCatalog(IEnumerable<ScheduleModel> catalog);
    }
}
