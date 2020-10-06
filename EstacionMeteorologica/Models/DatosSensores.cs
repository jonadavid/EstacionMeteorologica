using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionMeteorologica.Models
{
    public class DatosSensores
    {
        public int Id { get; set; }
        public float Temperatura { get; set; }
        public float Humedad { get; set; }
        public float Radiacion { get; set; }
        public float Precipitacion_lluvia { get; set; }
        public float Sensacion_termica { get; set; }
        public DateTime Fecha { get; set; }
        public int Id_estacion { get; set; }
    }
}
