using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Models
{
    internal class CategoryModel
    {
        public String name;
        public long id;

        public CategoryModel(Category cat)
        {
            name = cat.Name;
            id = cat.Id.Value;
        }
    }
}
