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
                    SqlCommand cmd = new SqlCommand("sp_lista_celulares", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CelularLista c = new CelularLista()
                        {
                            idcel = reader.GetString(0),
                            idmarca = reader.GetString(1),
                            modelo = reader.GetString(2),
                            precio = reader.GetDecimal(3),
                            idgama = reader.GetString(4),
                            stock = reader.GetInt32(5),
                            idsistema = reader.GetString(6),
                            idestado = reader.GetString(7)
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

        public ActionResult Select(string id, int cantidad)
        {
            CelularLista celular = buscarCelular(id);
            if (celular == null)
            {
                return new NotFoundResult();
            }
            if (celular.stock < cantidad)
            {
                return new BadRequestObjectResult("No hay suficiente stock");
            }
            celular.stock -= cantidad;
            return new OkObjectResult(celular);
        }

        //ELIMINAR CELULAR
        public string eliminarCelular(string id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_elimnar_celular", cn);
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
        public string mergeCelulares(CelularCrear celular)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_cadenaDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualiza_o_crea_celular", cn);
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
                    mensaje = $"Se inserto {totalRegistro} celular";
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
