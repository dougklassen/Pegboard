namespace DougKlassen.Pegboard.Models
{
    internal class ObjectStylesModel
    {
        public string Name { get; set; }
        public int? ProjectionLineweight { get; set; }
        public ColorModel LineColor { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public bool Delete { get; set; }
    }
}
