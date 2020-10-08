using System;

namespace EstacionMeteorologica.Models
{
    public class ItemReporte
    {
      
        public String NombreProvincia { get; set; }
        public String Ciudad { get; set; }
        public float Temperatura { get; set; }
        public float Humedad { get; set; }
        public float Precipitacion { get; set; }
        public float Radiacion { get; set; }
        public float SensacionTermica { get; set; }
        public DateTime Fecha { get; set; }

    }
}