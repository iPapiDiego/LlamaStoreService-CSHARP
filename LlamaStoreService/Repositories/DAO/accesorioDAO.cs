using LlamaStoreService.Models.Accesorios;
using Microsoft.Data.SqlClient;
using System.Data;

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
        public IEnumerable<ListaAccesorio> listaDeAccesorios()
        {
            List<ListaAccesorio> list = new List<ListaAccesorio>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_accesorios", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListaAccesorio ac = new ListaAccesorio()
                        {
                            idacce = reader.GetInt32(0),
                            tipo = reader.GetString(1),
                            nombre_marca = reader.GetString(2),
                            modelo = reader.GetString(3),
                            precio = reader.GetDecimal(4),
                            precio_oferta = reader.IsDBNull(4) ? 0 : reader.GetDecimal(5),
                            stock = reader.GetInt32(6),
                            descripcion = reader.GetString(7),
                            created_at = reader.GetDateTime(8),
                            updated_at = reader.GetDateTime(9)
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

        public ListaAccesorio buscarAccesorio(int id)
        {
            return listaDeAccesorios().Where(v => v.idacce == id).FirstOrDefault();
        }

        //ACTUALIZAR O CREAR CELULARES
        public string agregarAccesorio(CrudAccesorio crudAccesorio)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_accesorio", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idtipo", crudAccesorio.idtipo);
                    cmd.Parameters.AddWithValue("@idmarca", crudAccesorio.idmarca);
                    cmd.Parameters.AddWithValue("@idmodelo", crudAccesorio.idmodelo);
                    cmd.Parameters.AddWithValue("@precio", crudAccesorio.precio);
                    cmd.Parameters.AddWithValue("@precio_oferta", crudAccesorio.precio_oferta);
                    cmd.Parameters.AddWithValue("@stock", crudAccesorio.stock);
                    cmd.Parameters.AddWithValue("@descripcion", crudAccesorio.descripcion);

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

        public string actualizarAccesorio(CrudAccesorio crudAccesorio)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_accesorio", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idacce", crudAccesorio.idacce);
                    cmd.Parameters.AddWithValue("@idtipo", crudAccesorio.idtipo);
                    cmd.Parameters.AddWithValue("@idmarca", crudAccesorio.idmarca);
                    cmd.Parameters.AddWithValue("@idmodelo", crudAccesorio.idmodelo);
                    cmd.Parameters.AddWithValue("@precio", crudAccesorio.precio);
                    cmd.Parameters.AddWithValue("@precio_oferta", crudAccesorio.precio_oferta);
                    cmd.Parameters.AddWithValue("@stock", crudAccesorio.stock);
                    cmd.Parameters.AddWithValue("@descripcion", crudAccesorio.descripcion);

                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Se actualizo {totalRegistro} accesorio.";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }

        //ELIMINAR CELULAR
        public string eliminarAccesorio(string id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_accesorio", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idacce", id);

                    cn.Open();
                    int totalRegistros = cmd.ExecuteNonQuery();
                    mensaje = $"Se elimino {totalRegistros} accesorio.";
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
