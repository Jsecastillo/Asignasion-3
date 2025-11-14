using System;

namespace FamiliaWebApp.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        public int DuenoId { get; set; }
        public string DuenoNombre { get; set; } // para mostrar join
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public decimal? Peso { get; set; }
        public string Alergias { get; set; }

        public int Edad
        {
            get
            {
                if (!FechaNacimiento.HasValue) return 0;
                var hoy = DateTime.Today;
                int edad = hoy.Year - FechaNacimiento.Value.Year;
                if (hoy < FechaNacimiento.Value.AddYears(edad)) edad--;
                return Math.Max(0, edad);
            }
        }
    }
}
