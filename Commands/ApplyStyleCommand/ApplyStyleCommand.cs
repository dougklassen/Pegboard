using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DougKlassen.Pegboard.Models;
using DougKlassen.Pegboard.Repositories;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ApplyStyleCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            String ttl = "Apply Style";
            IVisibilitySettingsRepo repo = new VisibilitySettingsJsonRepo();
            VisibilitySettings VisibilitySettings = repo.LoadSettings();

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = commandData.Application.ActiveUIDocument.ActiveView;
            IEnumerable<ElementId> selectedElementIds;

            if (uiDoc.Selection.GetElementIds().Count == 0)
            {
                TaskDialog.Show("Apply Styles", "You must select elements to apply styles to");
                return Result.Failed;
            }
            else
            {
                selectedElementIds = uiDoc.Selection.GetElementIds();
            }

            OverrideGraphicSettings settingsToApply = VisibilitySettings.CurrentOverrideStyle.GetOverride();
            using (Transaction t = new Transaction(dbDoc))
            {
                t.Start("Viz-" + ttl);
                foreach (ElementId id in selectedElementIds)
                {
                    currentView.SetElementOverrides(id, settingsToApply);
                }
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
