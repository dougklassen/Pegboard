using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class SetViewTitlesCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;

            IEnumerable<View> docViews = new FilteredElementCollector(dbDoc)
                .OfCategory(BuiltInCategory.OST_Views)
                .Cast<View>()
                .Where(v => !v.IsTemplate);

            int setViewTitlesCounter = 0;

            using (Transaction t = new Transaction(dbDoc, "Set Title on Sheet value"))
            {
                t.Start();

                foreach (View v in docViews)
                {
                    Parameter p = v.LookupParameter("Title on Sheet");
                    if (null != p
                            && !p.IsReadOnly
                            && String.IsNullOrWhiteSpace(p.AsString()))
                    {
                        Parameter n = v.LookupParameter("View Name");
                        p.Set(n.AsString());
                        setViewTitlesCounter++;
                    }
                }

                t.Commit();
            }

            TaskDialog.Show("Result", setViewTitlesCounter + " view titles set");

            return Result.Succeeded;
        }
    }
}
