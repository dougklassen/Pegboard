﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DougKlassen.Pegboard.Helpers;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ResetGraphicsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string ttl = "Reset Graphic Overrides";
            string msg = string.Empty;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = commandData.Application.ActiveUIDocument.ActiveView;
            msg += "Current view: " + currentView.Name + '\n';
            if (!currentView.AreGraphicsOverridesAllowed())
            {
                msg += "Graphic overrides are not allowed in the current view";
                TaskDialog.Show(ttl, msg);
                return Result.Failed;
            }

            List<Element> elementsToReset = PegboardHelpers.GetAllElements(dbDoc).ToList();
            msg += "All element count: " + elementsToReset.Count + '\n';

            using (Transaction t = new Transaction(dbDoc))
            {
                t.Start("Reset graphics for " + currentView.Name);

                foreach (Element e in elementsToReset)
                {
                    currentView.SetElementOverrides(e.Id, new OverrideGraphicSettings());
                }

                t.Commit();
            }

            TaskDialog.Show(ttl, msg);
            return Result.Succeeded;
        }
    }
}
