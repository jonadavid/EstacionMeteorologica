using System;

namespace EstacionMeteorologica.Models
{
    public class Item
    {
        public string Id { get; set; }
        public String NombreProvincia { get; set; }
        public String Ciudad { get; set; }
        public float Temperatura { get; set; }
        public float Humedad { get; set; }
        public float Precipitacion { get; set; }
        public float Radiacion { get; set; }
        public float SensacionTermica { get; set; }
        public DateTime Fecha { get; set; }

        public String Text { get; set; }

        public String Description { get; set; }
    }
}