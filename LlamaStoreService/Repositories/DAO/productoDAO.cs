using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LlamaStoreService.Repositories.DAO
{
    public class productoDAO
    {
        private readonly string _cadenaDB;

        public productoDAO()
        {
            _cadenaDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        //LISTA DE CELULARES
        public IEnumerable<CelularLista> listaDeCelulares()
        {
            List<CelularLista> list = new List<CelularLista>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_productos", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CelularLista c = new CelularLista()
                        {
                            idcel = reader.GetString(0),
                            idmarca = reader.GetString(1),
                            modelo = reader.GetString(2),
                            idgama = reader.GetString(3),
                            precio = reader.GetDecimal(4),
                            idsistema = reader.GetString(5),
                            stock = reader.GetInt32(6),
                            especificaciones = reader.GetString(7)
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

        //BUSCAR CELULARES
        public CelularLista buscarCelular(string id)
        {
            return listaDeCelulares().Where(v => v.idcel == id).FirstOrDefault();
        }

       

        //ELIMINAR CELULAR
        public string eliminarCelular(string id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_celular", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcel", id);

                    cn.Open();
                    int totalRegistros = cmd.ExecuteNonQuery();
                    mensaje = $"Se elimino {totalRegistros} celular";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }

        //ACTUALIZAR O CREAR CELULARES
        public string agregarCelulares(CelularCrear celular)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_guardar_celular", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcel", celular.idcel);
                    cmd.Parameters.AddWithValue("@idmarca", celular.idmarca);
                    cmd.Parameters.AddWithValue("@modelo", celular.modelo);
                    cmd.Parameters.AddWithValue("@precio", celular.precio);
                    cmd.Parameters.AddWithValue("@idgama", celular.idgama);
                    cmd.Parameters.AddWithValue("@stock", celular.stock);
                    cmd.Parameters.AddWithValue("@idsistema", celular.idsistema);
                    

                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Se agrego {totalRegistro} celular";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }

        public string actualizarCelulares(CelularCrear celular)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_guardar_celular", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcel", celular.idcel);
                    cmd.Parameters.AddWithValue("@idmarca", celular.idmarca);
                    cmd.Parameters.AddWithValue("@modelo", celular.modelo);
                    cmd.Parameters.AddWithValue("@precio", celular.precio);
                    cmd.Parameters.AddWithValue("@idgama", celular.idgama);
                    cmd.Parameters.AddWithValue("@stock", celular.stock);
                    cmd.Parameters.AddWithValue("@idsistema", celular.idsistema);
                    cmd.Parameters.AddWithValue("@idestado", celular.idestado);

                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Se actualizo {totalRegistro} celular";
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
