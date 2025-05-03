using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Productos;
using Microsoft.Data.SqlClient;

namespace LlamaStoreService.Repositories.DAO
{
    public class accesorioDAO
    {
        private readonly string _cadenaDB;

        public accesorioDAO()
        {
            _cadenaDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        //LISTA DE ACCESORIOS
        public IEnumerable<Accesorio> listaDeAccesorios()
        {
            List<Accesorio> list = new List<Accesorio>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_accesorios", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Accesorio ac = new Accesorio()
                        {
                            idacce = reader.GetInt32(0),
                            idtipo = reader.GetString(1),
                            idmarca = reader.GetString(2),
                            idmodelo = reader.GetString(3),
                            precio = reader.GetDecimal(4),
                            stock = reader.GetInt32(5),
                            descripcion = reader.GetString(6)
                        }; 
                        list.Add(ac);
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
