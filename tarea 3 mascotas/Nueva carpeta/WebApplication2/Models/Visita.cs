using System;

namespace FamiliaWebApp.Models
{
    public class Visita
    {
        public int VisitaId { get; set; }
        public int MascotaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public string Sintomas { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Veterinario { get; set; }
    }
}
