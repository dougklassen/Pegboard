using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Models
{
    internal class ColorModel
    {
        public byte? Red { get; set; }
        public byte? Green { get; set; }
        public byte? Blue { get; set; }

        public ColorModel()
        {
            Red = null;
            Green = null;
            Blue = null;
        }

        public ColorModel(Color color)
        {
            if (color.IsValid)
            {
                Red = color.Red;
                Green = color.Green;
                Blue = color.Blue;
            }
            else
            {
                Red = null;
                Green = null;
                Blue = null;
            }
        }

        public Color GetColor()
        {
            if (Red.HasValue && Green.HasValue && Blue.HasValue)
            {
                return new Color(Red.Value, Green.Value, Blue.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
