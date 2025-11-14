using FamiliaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace FamiliaWebApp.Data
{
    public class DuenoRepositorio
    {
        private readonly string _cn;

        public DuenoRepositorio()
        {
            _cn = ConfigurationManager.ConnectionStrings["Veterinaria"].ConnectionString;
        }

        public IEnumerable<Dueno> ObtenerTodos()
        {
            var lista = new List<Dueno>();
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand("SELECT DuenoId, Nombre, Telefono, Correo, Direccion FROM Duenos ORDER BY Nombre", cn))
            {
                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(new Dueno
                        {
                            DuenoId = (int)r["DuenoId"],
                            Nombre = r["Nombre"] as string,
                            Telefono = r["Telefono"] as string,
                            Correo = r["Correo"] as string,
                            Direccion = r["Direccion"] as string
                        });
                    }
                }
            }
            return lista;
        }

        public Dueno ObtenerPorId(int id)
        {
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand("SELECT DuenoId, Nombre, Telefono, Correo, Direccion FROM Duenos WHERE DuenoId=@id", cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new Dueno
                        {
                            DuenoId = (int)r["DuenoId"],
                            Nombre = r["Nombre"] as string,
                            Telefono = r["Telefono"] as string,
                            Correo = r["Correo"] as string,
                            Direccion = r["Direccion"] as string
                        };
                    }
                }
            }
            return null;
        }

        public int Insertar(Dueno d)
        {
            using (var cn = new SqlConnection(_cn))
            using (var cmd = new SqlCommand("INSERT INTO Duenos (Nombre, Telefono, Correo, Direccion) OUTPUT INSERTED.DuenoId VALUES (@Nombre,@Telefono,@Correo,@Direccion)", cn))
            {
                cmd.Parameters.AddWithValue("@Nombre", d.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", (object)d.Telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", (object)d.Correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", (object)d.Direccion ?? DBNull.Value);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
