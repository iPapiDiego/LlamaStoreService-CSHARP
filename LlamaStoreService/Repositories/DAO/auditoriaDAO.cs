using LlamaStoreService.Models.Auditos;
using Microsoft.Data.SqlClient;

namespace LlamaStoreService.Repositories.DAO
{
    public class auditoriaDAO
    {
        private readonly string _cadenaDB;

        public auditoriaDAO()
        {
            _cadenaDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        //LISTA DE CELULARES
        public IEnumerable<Auditoria> ListaCambios()
        {
            List<Auditoria> list = new List<Auditoria>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_auditoria", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Auditoria a = new Auditoria()
                        {
                            idauditoria = reader.GetInt32(0),
                            tabla_afectada = reader.GetString(1),
                            id_registro = reader.GetString(2),
                            accion = reader.GetString(3),
                            valores_anteriores = reader.GetString(4),
                            valores_nuevos = reader.GetString(5),
                            usuario = reader.GetString(6),
                            fecha = reader.GetDateTime(7),
                        };
                        list.Add(a);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return list;
        }
    }
}
