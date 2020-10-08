using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionMeteorologica.Models
{
    public class RequestReporteFechas
    {
        public int Provincia { get; set; }
        public int Ciudad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }
}
