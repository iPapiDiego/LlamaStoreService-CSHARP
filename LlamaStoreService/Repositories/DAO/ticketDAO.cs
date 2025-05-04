
using LlamaStoreService.Models.Tickets;
using Microsoft.Data.SqlClient;

namespace LlamaStoreService.Repositories.DAO
{
    public class ticketDAO
    {
        private readonly string _cadenaDB;

        public ticketDAO()
        {
            _cadenaDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        //LISTA DE ACCESORIOS
        public IEnumerable<ListaBoleta> listaVentas()
        {
            List<ListaBoleta> list = new List<ListaBoleta>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_boletas_completas", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListaBoleta ac = new ListaBoleta()
                        {
                            num_bol = reader.GetString(0),
                            fch_bol = reader.GetDateTime(1),
                            cod_cliente = reader.GetInt32(2),
                            nombre = reader.GetString(3),
                            subtotal = reader.GetDecimal(4),
                            igv = reader.GetDecimal(5),
                            total = reader.GetDecimal(6),
                            metodo_pago = reader.GetString(7),
                            estado_pago = reader.GetInt32(8),
                            iddetalle = reader.GetInt32(9),
                            tipo_producto = reader.GetString(10),
                            id_producto = reader.GetString(11),
                            cantidad = reader.GetInt32(12),
                            precio_unitario = reader.GetDecimal(13),
                            descuento = reader.GetDecimal(14),
                            precio_final = reader.GetDecimal(15)
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
