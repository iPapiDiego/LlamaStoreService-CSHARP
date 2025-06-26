using LlamaStoreService.Models.Car;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LlamaStoreService.Repositories.DAO
{
    public class carritoDAO
    {

        private readonly string _cadenaDB;

        public carritoDAO()
        {
            _cadenaDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        public string agregarAlCarrito(Carrito carrito)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_agregar_al_carrito", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", carrito.codigo);
                    cmd.Parameters.AddWithValue("@idcel", carrito.idcel);
                    cmd.Parameters.AddWithValue("@modelo", carrito.modelo);
                    cmd.Parameters.AddWithValue("@cantidad", carrito.cantidad);
                    cmd.Parameters.AddWithValue("@precio_oferta", carrito.precio_oferta);


                    cn.Open();
                    int totalRegistros = cmd.ExecuteNonQuery();
                    mensaje = $"Se agrego {totalRegistros} celulares ";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }

            }
            return mensaje;
        }


        public IEnumerable<Carrito> ListadoPorId(int codigo)
        {
            List<Carrito> list = new List<Carrito>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_obtener_carrito_por_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())

                    {

                        Carrito c = new Carrito()
                        {
                            idCarrito = reader.GetInt32(0),
                            codigo = reader.GetInt32(1),
                            idcel = reader.GetString(2),
                            modelo = reader.GetString(3),
                            cantidad = reader.GetInt32(4),
                            precio_oferta = reader.GetDecimal(5)
                        };
                        list.Add(c);

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






        public string eliminarItemCarrito(int idCarrito)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_item_carrito", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCarrito", idCarrito);

                    cn.Open();
                    int totalRegistros = cmd.ExecuteNonQuery();
                    if (totalRegistros >= 2)
                    {
                        mensaje = $"Se elimino {totalRegistros} elenento de carrito";
                    }
                    else
                    {
                        mensaje = $"Se elimino {totalRegistros} elenento de carrito";
                    }
                }
                catch (Exception ex)
                {

                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }
    }
}
