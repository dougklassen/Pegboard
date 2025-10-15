using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DougKlassen.Pegboard.Helpers;
using DougKlassen.Pegboard.Models;
using DougKlassen.Pegboard.Repositories;
using Microsoft.Win32;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ExportParametersCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;

            IEnumerable<ParameterElement> userParams =
                new FilteredElementCollector(dbDoc)
                .OfClass(typeof(ParameterElement))
                .ToElements()
                .Cast<ParameterElement>();

            List<ParameterModel> paramData = new List<ParameterModel>();
            BindingMap map = dbDoc.ParameterBindings;

            foreach (ParameterElement param in userParams)
            {
                paramData.Add(new ParameterModel(param, map));
            }

            IEnumerable<Element> allElements = PegboardHelpers.GetAllElements(dbDoc);
            foreach (BuiltInParameter builtIn in Enum.GetValues(typeof(BuiltInParameter)))
            {
                paramData.Add(ParameterModel.GetBuiltInParameter(builtIn, map, allElements));
            }

            //TODO: autoincrement file name
            //TODO: cross reference with categories

            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                FileName = dbDoc.Title + "-parameters-" + PegboardHelpers.GetTimeStamp() + ".json",
                Filter = "JSON file|*.json",
                Title = "Save Parameters Catalog"
            };
            bool? result = saveDialog.ShowDialog();
            if (saveDialog.FileName == String.Empty || result != true)
            {
                return Result.Cancelled;
            }
            IParameterCatalogRepo paramRepo = new ParameterCatalogJsonRepo(saveDialog.FileName);
            paramRepo.WriteParameterCatalog(paramData);

            return Result.Succeeded;
        }
    }
}
