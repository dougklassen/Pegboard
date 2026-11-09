using DougKlassen.Pegboard.Models;
using System.Collections.Generic;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IScheduleCatalogRepo
    {
        IEnumerable<ScheduleModel> LoadScheduleCatalog();
        void WriteScheduleCatalog(IEnumerable<ScheduleModel> catalog);
    }
}
