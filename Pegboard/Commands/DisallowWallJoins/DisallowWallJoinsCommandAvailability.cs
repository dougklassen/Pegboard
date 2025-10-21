using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Pegboard.Commands
{
    internal class DisallowWallJoinsCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            var uiDoc = applicationData.ActiveUIDocument;

            if (null != uiDoc)
            {
                var dbDoc = uiDoc.Document;
                if (0 != uiDoc.Selection.GetElementIds().Count)
                {
                    if (selectedCategories.Contains(Category.GetCategory(dbDoc, BuiltInCategory.OST_Walls)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
