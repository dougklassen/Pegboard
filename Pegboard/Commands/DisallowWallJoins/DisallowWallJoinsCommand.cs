using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class DisallowWallJoinsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document dbDoc = uiDoc.Document;

            //get the currently selected elements
            var selectedElements = uiDoc.Selection.GetElementIds();

            using (Transaction t = new Transaction(dbDoc, "Disallow wall joins"))
            {
                t.Start();

                foreach (var elementID in selectedElements)
                {
                    var wall = dbDoc.GetElement(elementID) as Wall;

                    //skip non-walls
                    if (null == wall) continue;

                    var locationLine = wall.Location as LocationCurve;

                    //skip walls that aren't defined by a LocationCurve
                    if (null == locationLine) continue;

                    //disallow joins at both ends. 0 is the index of the beginning and 1 of the end.
                    WallUtils.DisallowWallJoinAtEnd(wall, 0);
                    WallUtils.DisallowWallJoinAtEnd(wall, 1);
                    //perform a geometry unjoin for good measure
                    locationLine.set_JoinType(0, JoinType.None);
                    locationLine.set_JoinType(1, JoinType.None);
                }

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
