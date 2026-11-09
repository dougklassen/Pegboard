using System.Collections.Generic;

namespace DougKlassen.Pegboard.Commands
{
    internal interface IQuantityScheduleTemplateRepo
    {
        List<QuantityScheduleTemplate> LoadTemplates();

        void WriteTemplates(List<QuantityScheduleTemplate> templates);
    }
}
