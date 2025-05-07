using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Users;
using LlamaStoreService.Models.Usuarios;
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

        public IEnumerable<CrudUsuario> listaDeUsuariosCompleto()
        {
            List<CrudUsuario> list = new List<CrudUsuario>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cade))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarUsuarios_todo", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CrudUsuario u = new CrudUsuario()
                        {
                            codigo = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido = reader.GetString(2),
                            correo = reader.GetString(3),
                            clave = reader.GetString(4),
                            fnacim = reader.GetDateTime(5),
                            idrol = reader.GetInt32(6),
                            estado = reader.GetInt32(7),
                            created_at = reader.GetDateTime(8),
                            updated_at = reader.GetDateTime(9),
                            
                            intentos_login = reader.GetInt32(10)
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

        public IEnumerable<Roll> listaRoles()
        {
            List<Roll> list = new List<Roll>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cade))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_roles", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Roll u = new Roll()
                        {
                            idroll = reader.GetInt32(0),
                            roll = reader.GetString(1)
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

        public CrudUsuario buscarUsuarioPorID(int id)
        {
            return listaDeUsuariosCompleto().Where(v => v.codigo == id).FirstOrDefault();
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

        public string actualizarUsuariosAdmin(CrudUsuario crudUsuario)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cade))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", crudUsuario.codigo);
                    cmd.Parameters.AddWithValue("@nombre", crudUsuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", crudUsuario.apellido);
                    cmd.Parameters.AddWithValue("@correo", crudUsuario.correo);
                    cmd.Parameters.AddWithValue("@clave", crudUsuario.clave);
                    cmd.Parameters.AddWithValue("@fnacim", crudUsuario.fnacim);
                    cmd.Parameters.AddWithValue("@idrol", crudUsuario.idrol);
                    cmd.Parameters.AddWithValue("@estado", crudUsuario.estado);

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

