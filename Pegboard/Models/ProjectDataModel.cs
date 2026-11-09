using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Models
{
    internal class ProjectDataModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProjectNumber { get; set; }
        public string Status { get; set; }
        public string ClientName { get; set; }
        public string FilePath { get; set; }
        public int? FileSize { get; set; }
        public int? ElementCount { get; set; }

        public ProjectDataModel(Document dbDoc)
        {
            ProjectInfo info = dbDoc.ProjectInformation;
            Name = info.Name;
            Address = info.Address;
            ProjectNumber = info.Number;
            Status = info.Status;
            ClientName = info.ClientName;
            FilePath = dbDoc.PathName;
        }
    }
}
