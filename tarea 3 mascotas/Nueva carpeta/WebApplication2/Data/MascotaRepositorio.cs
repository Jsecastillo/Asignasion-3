using FamiliaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace FamiliaWebApp.Data
{
    public class MascotaRepositorio
    {
        private readonly string _cn;

        public MascotaRepositorio()
        {
            _cn = ConfigurationManager.ConnectionStrings["Veterinaria"].ConnectionString;
        }

        public IEnumerable<Mascota> ObtenerTodas()
        {
            var lista = new List<Mascota>();
            string sql = @"SELECT m.MascotaId, m.DuenoId, m.Nombre, m.Especie, m.Raza, m.FechaNacimiento, m.Peso, m.Alergias, d.Nombre AS DuenoNombre
                           FROM Mascotas m
                           LEFT JOIN Duenos d ON m.DuenoId = d.DuenoId
                           ORDER BY m.Nombre";
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(new Mascota
                        {
                            MascotaId = (int)r["MascotaId"],
                            DuenoId = (int)r["DuenoId"],
                            Nombre = r["Nombre"] as string,
                            Especie = r["Especie"] as string,
                            Raza = r["Raza"] as string,
                            FechaNacimiento = r["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : (DateTime)r["FechaNacimiento"],
                            Peso = r["Peso"] == DBNull.Value ? (decimal?)null : (decimal)r["Peso"],
                            Alergias = r["Alergias"] as string,
                            DuenoNombre = r["DuenoNombre"] as string
                        });
                    }
                }
            }
            return lista;
        }

        public void Insertar(Mascota m)
        {
            string sql = @"INSERT INTO Mascotas (DuenoId, Nombre, Especie, Raza, FechaNacimiento, Peso, Alergias)
                           VALUES (@DuenoId,@Nombre,@Especie,@Raza,@FechaNacimiento,@Peso,@Alergias)";
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@DuenoId", m.DuenoId);
                cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
                cmd.Parameters.AddWithValue("@Especie", (object)m.Especie ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Raza", (object)m.Raza ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", (object)m.FechaNacimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Peso", (object)m.Peso ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Alergias", (object)m.Alergias ?? DBNull.Value);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
