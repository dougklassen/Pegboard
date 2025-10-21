using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DougKlassen.Pegboard.Helpers;
using DougKlassen.Pegboard.Models;
using DougKlassen.Pegboard.Repositories;
using Microsoft.Win32;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ExportCategoriesCommand : IExternalCommand
    {
        //TODO: identify user defined categories
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;

            List<CategoryModel> categoryData = new List<CategoryModel>();
            Categories categories = dbDoc.Settings.Categories;
            foreach (Category cat in categories)
            {
                categoryData.Add(new CategoryModel(cat));
            }

            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                FileName = dbDoc.Title + "-categories-" + PegboardHelpers.GetTimeStamp() + ".json",
                Filter = "JSON file|*.json",
                Title = "Save Categories Catalog"
            };
            bool? result = saveDialog.ShowDialog();
            if (saveDialog.FileName == String.Empty || result != true)
            {
                return Result.Cancelled;
            }
            ICategoryCatalogRepo catRepo = new CategoryCatalogJsonRepo(saveDialog.FileName);
            catRepo.WriteScheduleCatalog(categoryData);

            return Result.Succeeded;
        }
    }
}
