using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DougKlassen.Pegboard.Models;
using DougKlassen.Pegboard.Helpers;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class PickUpStyleCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            IVisibilitySettingsRepo repo = new VisibilitySettingsJsonRepo();
            VisibilitySettings visibilitySettings = repo.LoadSettings();

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = commandData.Application.ActiveUIDocument.ActiveView;
            ElementId sourceElementId;

            if (uiDoc.Selection.GetElementIds().Count == 1)
            {
                sourceElementId = uiDoc.Selection.GetElementIds().First();
            }
            else
            {
                try
                {
                    sourceElementId = uiDoc.Selection.PickObject(ObjectType.Element).ElementId;
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return Result.Cancelled;
                }
            }
            OverrideGraphicSettings selectedOverrides = currentView.GetElementOverrides(sourceElementId);

            visibilitySettings.CurrentOverrideStyle = selectedOverrides.GetVisibilityModel();
            repo.WriteSettings(visibilitySettings);

            return Result.Succeeded;
        }
    }
}
