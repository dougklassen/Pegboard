using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Pegboard.Commands
{
    /// <summary>
    /// Always make a command available, including when no project is open
    /// </summary>
    internal class AlwaysAvailableCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
