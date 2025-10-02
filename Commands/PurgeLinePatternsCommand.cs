using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DougKlassen.Revit.Perfect.Interface;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class PurgeLinePatternsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;

            IEnumerable<LinePatternElement> docLinePatterns = new FilteredElementCollector(dbDoc)
                .OfClass(typeof(LinePatternElement))
                .AsEnumerable()
                .Cast<LinePatternElement>();

            RegexSelectElementsWindow purgeWindow = new RegexSelectElementsWindow(dbDoc, typeof(LinePatternElement));
            //TODO: allow storing and retrieving history
            purgeWindow.SelectRegExString = @"^IMPORT-.*$";

            purgeWindow.ShowDialog();
            if(purgeWindow.DialogResult == false)
            {
                return Result.Cancelled;
            }

            ICollection<ElementId> patternsToDelete = new List<ElementId>();
            ElementId match;
            foreach (String patName in purgeWindow.MatchingElementsListBox.Items)
            {
                match = docLinePatterns.Where(p => patName == p.Name).FirstOrDefault().Id;
                if (match != null)
                {
                    patternsToDelete.Add(match);
                }
            }

            using (Transaction t = new Transaction(dbDoc, "Purge line patterns"))
            {
                t.Start();
                dbDoc.Delete(patternsToDelete);
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
