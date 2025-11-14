using FamiliaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace FamiliaWebApp.Data
{
    public class VisitaRepositorio
    {
        private readonly string _cn;

        public VisitaRepositorio()
        {
            _cn = ConfigurationManager.ConnectionStrings["Veterinaria"].ConnectionString;
        }

        public IEnumerable<Visita> ObtenerPorMascota(int mascotaId)
        {
            var lista = new List<Visita>();
            string sql = @"SELECT VisitaId, MascotaId, Fecha, Motivo, Sintomas, Diagnostico, Tratamiento, Veterinario
                           FROM Visitas
                           WHERE MascotaId = @mascotaId
                           ORDER BY Fecha DESC";
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@mascotaId", mascotaId);
                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(new Visita
                        {
                            VisitaId = (int)r["VisitaId"],
                            MascotaId = (int)r["MascotaId"],
                            Fecha = (DateTime)r["Fecha"],
                            Motivo = r["Motivo"] as string,
                            Sintomas = r["Sintomas"] as string,
                            Diagnostico = r["Diagnostico"] as string,
                            Tratamiento = r["Tratamiento"] as string,
                            Veterinario = r["Veterinario"] as string
                        });
                    }
                }
            }
            return lista;
        }

        public void Insertar(Visita v)
        {
            string sql = @"INSERT INTO Visitas (MascotaId, Fecha, Motivo, Sintomas, Diagnostico, Tratamiento, Veterinario)
                           VALUES (@MascotaId, @Fecha, @Motivo, @Sintomas, @Diagnostico, @Tratamiento, @Veterinario)";
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@MascotaId", v.MascotaId);
                cmd.Parameters.AddWithValue("@Fecha", v.Fecha);
                cmd.Parameters.AddWithValue("@Motivo", (object)v.Motivo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sintomas", (object)v.Sintomas ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Diagnostico", (object)v.Diagnostico ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tratamiento", (object)v.Tratamiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Veterinario", (object)v.Veterinario ?? DBNull.Value);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
