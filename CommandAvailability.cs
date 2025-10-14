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

    /// <summary>
    /// Never make a command available. Primarily for placeholder commands that haven't been fully implemented yet.
    /// </summary>
    internal class NeverAvailableCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return false;
        }
    }

    /// <summary>
    /// Make command available if the current view is a sheet or the current selection contains at least one sheet
    /// </summary>
    internal class SheetCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            UIDocument uiDoc = applicationData.ActiveUIDocument;
            Document dbDoc = applicationData.ActiveUIDocument.Document;

            if (
                selectedCategories.Contains(Category.GetCategory(dbDoc, BuiltInCategory.OST_Sheets)) ||
                uiDoc.ActiveView is ViewSheet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
