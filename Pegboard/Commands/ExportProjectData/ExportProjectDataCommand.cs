using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DougKlassen.Pegboard.Helpers;
using DougKlassen.Pegboard.Models;
using DougKlassen.Pegboard.Repositories;
using Microsoft.Win32;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ExportProjectDataCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;

            ProjectDataModel projectData = new ProjectDataModel(dbDoc);

            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                FileName = dbDoc.Title + "-project data-" + PegboardHelpers.GetTimeStamp() + ".json",
                Filter = "JSON file|*.json",
                Title = "Save Project Data Catalog"
            };
            bool? result = saveDialog.ShowDialog();
            if (saveDialog.FileName == String.Empty || result != true)
            {
                return Result.Cancelled;
            }
            IProjectDataRepo catRepo = new ProjectDataJsonRepo(saveDialog.FileName);
            catRepo.WriteProjectDataCatalog(projectData);

            return Result.Succeeded;
        }
    }
}
