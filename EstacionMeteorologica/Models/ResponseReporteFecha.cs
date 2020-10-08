using EstacionMeteorologica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionMeteorologica.Models
{
    public class ResponseReporteFecha: DatosSensores
    {
        DatosSensores Sensores { get; set; }
        public String NombreProvincia { get; set; }
        public String Ciudad { get; set; }
    }
}
