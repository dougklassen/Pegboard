using DougKlassen.Pegboard.Models;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IObjectStylesRepo
    {
        IEnumerable<ObjectStylesModel> LoadObjectStyles();
        void WriteObjectStyles(IEnumerable<ObjectStylesModel> catalog);
    }
}
