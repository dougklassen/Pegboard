using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Pegboard.Commands
{
    internal class UnflipCommandAvailability : IExternalCommandAvailability
    {
        /// <summary>
        /// Command availability method for the Unflip command. Checks that at least on element is selected
        /// </summary>
        /// <param name="applicationData">A reference giving access to the Revit user interface</param>
        /// <param name="selectedCategories">Categories of elements included in the selection</param>
        /// <returns></returns>
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            UIDocument uiDoc = applicationData.ActiveUIDocument;

            //TODO: check that the selection includes elements to which the logic of the command applies
            if (null != uiDoc)
            {
                if (0 != uiDoc.Selection.GetElementIds().Count)
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
    }
}
