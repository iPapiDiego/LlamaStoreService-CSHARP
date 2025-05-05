using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Estatus;
using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Products;
using LlamaStoreService.Models.Usuarios;
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
                    SqlCommand cmd = new SqlCommand("sp_listar_productos_totales", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CelularLista c = new CelularLista()
                        {
                            idcel = reader.GetString(0),
                            nombre_marca = reader.GetString(1),
                            modelo = reader.GetString(2),
                            tipo_sistema = reader.GetString(3),
                            precio = reader.GetDecimal(4),
                            precio_oferta = reader.IsDBNull(4) ? 0 : reader.GetDecimal(5),
                            tipo_gama = reader.GetString(6),
                            stock = reader.GetInt32(7),
                            descripcion = reader.GetString(8),
                            created_at = reader.GetDateTime(9),
                            updated_at = reader.GetDateTime(10)
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
                    cmd.Parameters.AddWithValue("@precio_oferta", celular.precio_oferta);
                    cmd.Parameters.AddWithValue("@idgama", celular.idgama);
                    cmd.Parameters.AddWithValue("@idsistema", celular.idsistema);
                    cmd.Parameters.AddWithValue("@especificaciones", celular.especificaciones);
                    cmd.Parameters.AddWithValue("@stock", celular.stock);


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
                    cmd.Parameters.AddWithValue("@precio_oferta", celular.precio_oferta);
                    cmd.Parameters.AddWithValue("@idgama", celular.idgama);
                    cmd.Parameters.AddWithValue("@idsistema", celular.idsistema);
                    cmd.Parameters.AddWithValue("@especificaciones", celular.especificaciones);
                    cmd.Parameters.AddWithValue("@stock", celular.stock);
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

        //LISTAS DE SUBTABLAS

        public IEnumerable<Gama> listaDeGamas()
        {
            List<Gama> list = new List<Gama>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_gamas", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Gama G = new Gama()
                        {
                            idgama = reader.GetInt32(0),
                            tipogama = reader.GetString(1),
                        };
                        list.Add(G);
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

        public IEnumerable<Marca> listaDeMarcas()
        {
            List<Marca> list = new List<Marca>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_marcas", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Marca G = new Marca()
                        {
                            idmarca = reader.GetInt32(0),
                            nombre_marca = reader.GetString(1),
                        };
                        list.Add(G);
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

        public IEnumerable<Sistema> listaDeSistemas()
        {
            List<Sistema> list = new List<Sistema>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_sistemas", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Sistema G = new Sistema()
                        {
                            idsistema = reader.GetInt32(0),
                            tipodesistema = reader.GetString(1),
                        };
                        list.Add(G);
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

        public IEnumerable<Estado> listaEstados()
        {
            List<Estado> list = new List<Estado>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_estados", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Estado e = new Estado()
                        {
                            idestado = reader.GetInt32(0),
                            descripcion = reader.GetString(1),
                        };
                        list.Add(e);
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
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_roles", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Roll e = new Roll()
                        {
                            idroll = reader.GetInt32(0),
                            roll = reader.GetString(1),
                        };
                        list.Add(e);
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

        public IEnumerable<Tipo> listaTiposAccesorios()
        {
            List<Tipo> list = new List<Tipo>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_tipoAcce", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tipo e = new Tipo()
                        {
                            idtipo = reader.GetInt32(0),
                            tipo = reader.GetString(1),
                        };
                        list.Add(e);
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

        public IEnumerable<Modelo> listaModelosAccesorios()
        {
            List<Modelo> list = new List<Modelo>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaDB))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_modeloAcce", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Modelo e = new Modelo()
                        {
                            idmodelo = reader.GetInt32(0),
                            modelo = reader.GetString(1),
                        };
                        list.Add(e);
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
