
using LlamaStoreService.Models.Products;
using LlamaStoreService.Models.Users;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LlamaStoreService.Repositories.DAO
{
    public class userDAO
    {
        private readonly string _cadenaDB;
        //XDDDD
        public userDAO()
        {
            _cadenaDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Usuario> listaDeUsuarios()
        {
            List<Usuario> list = new List<Usuario>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_lista_usuarios", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario u = new Usuario()
                        {
                            codigo = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido = reader.GetString(2),
                            correo = reader.GetString(3),
                            clave = reader.GetString(4),
                            fnacim = reader.GetDateTime(5),
                            tipo = reader.GetInt32(6),
                            estado = reader.GetInt32(7)
                        };
                        list.Add(u);
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

        public Usuario buscarUsuarioPorID(int id)
        {
            return listaDeUsuarios().Where(v => v.codigo == id).FirstOrDefault();
        }

        public string eliminarUsuario(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_elimnar_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", id);

                    cn.Open();
                    int totalRegistros = cmd.ExecuteNonQuery();
                    mensaje = $"Se elimino {totalRegistros} usuario";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }

        public string mergeUsuarios(Usuario usuario)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualiza_o_crea_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", usuario.codigo);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.clave);
                    cmd.Parameters.AddWithValue("@fnacim", usuario.fnacim);
                    cmd.Parameters.AddWithValue("@tipo", usuario.tipo);
                    cmd.Parameters.AddWithValue("@estado", usuario.estado);

                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Se inserto {totalRegistro} usuario";
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
