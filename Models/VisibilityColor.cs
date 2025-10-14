using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Models
{
    internal class VisibilityColor
    {
        public Byte? Red { get; set; }
        public Byte? Green { get; set; }
        public Byte? Blue { get; set; }

        public VisibilityColor()
        {
            Red = null;
            Green = null;
            Blue = null;
        }

        public VisibilityColor(Color color)
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
