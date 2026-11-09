using DougKlassen.Pegboard.Models;
using System.Collections.Generic;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IObjectStylesRepo
    {
        IEnumerable<ObjectStylesModel> LoadObjectStyles();
        void WriteObjectStyles(IEnumerable<ObjectStylesModel> catalog);
    }
}
