using System.IO;

namespace DougKlassen.Pegboard.Commands
{
    internal class QuantityScheduleParseException : FileFormatException
    {
        public QuantityScheduleParseException(string message) : base(message) { }
    }
}
