using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionMeteorologica.Models
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre_Ciudad { get; set; }
        public int Estado_Cuidad { get; set; }
        public int Id_Provincia { get; set; }
    }
}
