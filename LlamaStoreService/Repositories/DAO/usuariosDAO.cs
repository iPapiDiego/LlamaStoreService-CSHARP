using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Users;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LlamaStoreService.Repositories.DAO
{
    public class usuariosDAO
    {

        private readonly string _cade;
        //XDDDD
        public usuariosDAO()
        {
            _cade = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Usuario> listaDeUsuarios()
        {
            List<Usuario> list = new List<Usuario>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cade))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarUsuariosBasico", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario u = new Usuario()
                        {
                            codigo = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido = reader.GetString(2),
                            correo = reader.GetString(3),
                            fnacim = reader.GetDateTime(4),
                            clave = reader.GetString(5)
                            
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

        public string agregarUsuarios(Usuario usuario)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cade))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", usuario.codigo);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.clave);
                    cmd.Parameters.AddWithValue("@fnacim", usuario.fnacim);
                    


                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Bienvenido, inicia secion para continuar.";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }

        public string actualizarUsuariosCliente(Usuario usuario)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cade))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", usuario.codigo);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.clave);
                    cmd.Parameters.AddWithValue("@fnacim", usuario.fnacim);
                    
                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Sus datos se actualizaron correctamente.";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return mensaje;
        }

        public string actualizarUsuariosAdmin(Usuario usuario)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cade))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", usuario.codigo);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.clave);
                    cmd.Parameters.AddWithValue("@fnacim", usuario.fnacim);
                    cmd.Parameters.AddWithValue("@idrol", usuario.idrol);
                    cmd.Parameters.AddWithValue("@estado", usuario.estado);

                    cn.Open();
                    int totalRegistro = cmd.ExecuteNonQuery();
                    mensaje = $"Se actualizo {totalRegistro} usuario";
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

