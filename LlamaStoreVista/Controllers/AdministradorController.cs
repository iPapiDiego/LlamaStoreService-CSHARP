using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Auditos;
using LlamaStoreService.Models.Estatus;
using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Products;
using LlamaStoreService.Models.Tickets;
using LlamaStoreService.Models.Users;
using LlamaStoreService.Models.Usuarios;
using LlamaStoreVista.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Linq;

namespace LlamaStoreVista.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly string conexionUser = "https://localhost:44331/api/Usuario/";
        private readonly string conexionProduc = "https://localhost:44331/api/Products/";
        private readonly string conexionAccesor = "https://localhost:44331/api/Accesorio/";
        private readonly string conexionBoleta = "https://localhost:44331/api/Boleta/";
        private readonly string conexionAuditorias = "https://localhost:44331/api/Auditoria/";



        public async Task<IActionResult> Dashboard(int page = 1)
        {
            List<Auditoria> temporal = new List<Auditoria>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAuditorias);
                HttpResponseMessage response = await client.GetAsync("getAuditoria");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Auditoria>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;

            // No restes 1 a la página, mantenla como viene
            ViewBag.page = page; // Ahora page es 1-based
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * (page - 1)).Take(fila)));
        }

        public async Task<IActionResult> ListaProductos(int page = 1)
        {
            List<Producto> temporal = new List<Producto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getCelulares");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Producto>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;

            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * (page - 1)).Take(fila)));
        }

        public async Task<IActionResult> ListaAccesorios(int page = 1)
        {
            List<Accesorio> temporal = new List<Accesorio>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getAccesorios");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Accesorio>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;

            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * (page - 1)).Take(fila)));
        }

        public async Task<IActionResult> ListaVentas(int page = 1)
        {
            List<ListaBoleta> temporal = new List<ListaBoleta>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionBoleta);
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getBoleta");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<ListaBoleta>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;

            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * (page - 1)).Take(fila)));
        }

        public async Task<IActionResult> ListaReportes(int page = 1)
        {
            List<ListaBoleta> temporal = new List<ListaBoleta>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionBoleta);
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getBoleta");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<ListaBoleta>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;

            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * (page - 1)).Take(fila)));
        }

        //=========================================================================

        public async Task<List<Gama>> ListaGamas()
        {
            List<Gama> temporal = new List<Gama>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage response = await client.GetAsync("getGamas");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Gama>>(apiresponse).ToList();
            }
            return temporal;
        }

        public async Task<List<Marca>> ListaMarcas()
        {
            List<Marca> temporal = new List<Marca>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage response = await client.GetAsync("GetMarcas");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Marca>>(apiresponse).ToList();
            }
            return temporal;
        }

        public async Task<List<Sistema>> ListaSistemas()
        {
            List<Sistema> temporal = new List<Sistema>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage response = await client.GetAsync("getSistemas");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Sistema>>(apiresponse).ToList();
            }
            return temporal;
        }

        public async Task<List<Estado>> ListaEstados()
        {
            List<Estado> temporal = new List<Estado>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage response = await client.GetAsync("getEstados");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Estado>>(apiresponse).ToList();
            }
            return temporal;
        }

        public async Task<IActionResult> AgregarProducto()
        {
            var gamas = await ListaGamas();
            ViewBag.gamas = new SelectList(gamas, "idgama", "tipogama");

            var marcas = await ListaMarcas();
            ViewBag.marcas = new SelectList(marcas, "idmarca", "nombre_marca");

            var sistemas = await ListaSistemas();
            ViewBag.sistemas = new SelectList(sistemas, "idsistema", "tipodesistema");

            var estados = await ListaEstados();
            ViewBag.estados = new SelectList(estados, "idestado", "descripcion");

            return View(await Task.Run(() => new ProductoCrear()));
        }


        [HttpPost]
        public async Task<IActionResult> AgregarProducto(ProductoCrear productoCrear)
        {
            try
            {
                if (productoCrear.ImagenFile == null || productoCrear.ImagenFile.Length == 0)
                {
                    TempData["mensaje"] = "Debe seleccionar una imagen válida";
                    return View(productoCrear);
                }

                // 1. Obtener nombre original completo
                var originalFileName = productoCrear.ImagenFile.FileName;
                var extension = Path.GetExtension(originalFileName);

                // 2. Validar extensión
                if (string.IsNullOrEmpty(extension) || !new[] { ".jpg", ".jpeg", ".png" }.Contains(extension.ToLower()))
                {
                    TempData["mensaje"] = "Solo se permiten imágenes JPG, JPEG o PNG";
                    return View(productoCrear);
                }

                // 3. Limpiar nombre de archivo (remover caracteres inválidos)
                var safeFileName = MakeValidFileName(originalFileName);

                // 4. Ruta de almacenamiento
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productos");
                Directory.CreateDirectory(imagesPath); // Asegurar que existe

                var fullPath = Path.Combine(imagesPath, safeFileName);

                // 5. Verificar si ya existe y agregar sufijo numérico
                int counter = 1;
                while (System.IO.File.Exists(fullPath))
                {
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(safeFileName);
                    extension = Path.GetExtension(safeFileName);
                    safeFileName = $"{fileNameWithoutExt}_{counter++}{extension}";
                    fullPath = Path.Combine(imagesPath, safeFileName);
                }

                // 6. Guardar archivo
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await productoCrear.ImagenFile.CopyToAsync(stream);
                }

                // 7. Asignar ruta relativa
                productoCrear.imagen = $"/img/productos/{safeFileName}";

                // ... (resto de tu código para guardar en la API)
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(conexionProduc);
                    var content = new StringContent(JsonConvert.SerializeObject(productoCrear),
                        Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("postAgregarCelular", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        // Limpiar archivo si falla la API
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        TempData["mensaje"] = "Error al guardar en la API";
                        return RedirectToAction("ListaProductos");
                    }
                }


                TempData["mensaje"] = "Producto Registrado";
                return RedirectToAction("ListaProductos");
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = $"Error: {ex.Message}";
                return View(productoCrear);
            }
        }

        // Función para limpiar nombres de archivo
        private string MakeValidFileName(string name)
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();
            var cleanName = new string(name
                .Where(ch => !invalidChars.Contains(ch))
                .ToArray())
                .Replace(" ", "_"); // Reemplazar espacios por guiones bajos

            return string.IsNullOrEmpty(cleanName) ? "imagen" + Path.GetExtension(name) : cleanName;
        }

        //EDITAR PRODUCTO
        public async Task<IActionResult> ActualizarProducto(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("ListaProductos");

            ProductoCrear vendedor = new ProductoCrear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage respons = await client.GetAsync("getBuscarCelular/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                vendedor = JsonConvert.DeserializeObject<ProductoCrear>(apiResponse);
            }

            var gamas = await ListaGamas();
            ViewBag.gamas = new SelectList(gamas, "idgama", "tipogama");

            var marcas = await ListaMarcas();
            ViewBag.marcas = new SelectList(marcas, "idmarca", "nombre_marca");

            var sistemas = await ListaSistemas();
            ViewBag.sistemas = new SelectList(sistemas, "idsistema", "tipodesistema");

            var estados = await ListaEstados();
            ViewBag.estados = new SelectList(estados, "idestado", "descripcion");

            return View(await Task.Run(() => vendedor));

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarProducto(ProductoCrear productoCrear)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                StringContent content = new StringContent(JsonConvert.SerializeObject(productoCrear), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActualizarCelular", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("ListaProductos");
        }

        // Acción que recibe el ID y elimina desde la API
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("ListaProductos");

            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage response = await client.DeleteAsync("deleteCelular/?id=" + id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                mensaje = apiResponse;
            }

            TempData["mensaje"] = mensaje;
            return RedirectToAction("ListaProductos");
        }

        //===================================================ACCESORIO======================================================
        //===================================================ACCESORIO======================================================
        //===================================================ACCESORIO======================================================
        //===================================================ACCESORIO======================================================
        //===================================================ACCESORIO======================================================
        //===================================================ACCESORIO======================================================
        //===================================================ACCESORIO======================================================

        public async Task<List<Tipo>> ListaTipoAcce()
        {
            List<Tipo> temporal = new List<Tipo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                HttpResponseMessage response = await client.GetAsync("getTiposAcce");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Tipo>>(apiresponse).ToList();
            }
            return temporal;
        }

        public async Task<List<Modelo>> ListaModeloAcce()
        {
            List<Modelo> temporal = new List<Modelo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                HttpResponseMessage response = await client.GetAsync("getModeloAcce");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Modelo>>(apiresponse).ToList();
            }
            return temporal;
        }



        public async Task<IActionResult> AgregarAccesorio()
        {
            var tipos = await ListaTipoAcce();
            ViewBag.tipos = new SelectList(tipos, "idtipo", "tipo");

            var modelos = await ListaModeloAcce();
            ViewBag.modelos = new SelectList(modelos, "idmodelo", "modelo");

            var marcas = await ListaMarcas();
            ViewBag.marcas = new SelectList(marcas, "idmarca", "nombre_marca");

            return View(await Task.Run(() => new CrudAccesorio()));
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAccesorio(CrudAccesorio crudAccesorio)
        {
            try
            {
                if (crudAccesorio.ImagenFile == null || crudAccesorio.ImagenFile.Length == 0)
                {
                    TempData["mensaje"] = "Debe seleccionar una imagen válida";
                    return View(crudAccesorio);
                }

                // 1. Obtener nombre original completo
                var originalFileName = crudAccesorio.ImagenFile.FileName;
                var extension = Path.GetExtension(originalFileName);

                // 2. Validar extensión
                if (string.IsNullOrEmpty(extension) || !new[] { ".jpg", ".jpeg", ".png" }.Contains(extension.ToLower()))
                {
                    TempData["mensaje"] = "Solo se permiten imágenes JPG, JPEG o PNG";
                    return View(crudAccesorio);
                }

                // 3. Limpiar nombre de archivo (remover caracteres inválidos)
                var safeFileName = MakeValidFileName(originalFileName);

                // 4. Ruta de almacenamiento
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "accesorios");
                Directory.CreateDirectory(imagesPath); // Asegurar que existe

                var fullPath = Path.Combine(imagesPath, safeFileName);

                // 5. Verificar si ya existe y agregar sufijo numérico
                int counter = 1;
                while (System.IO.File.Exists(fullPath))
                {
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(safeFileName);
                    extension = Path.GetExtension(safeFileName);
                    safeFileName = $"{fileNameWithoutExt}_{counter++}{extension}";
                    fullPath = Path.Combine(imagesPath, safeFileName);
                }

                // 6. Guardar archivo
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await crudAccesorio.ImagenFile.CopyToAsync(stream);
                }

                // 7. Asignar ruta relativa
                crudAccesorio.imagen = $"/img/accesorios/{safeFileName}";

                // ... (resto de tu código para guardar en la API)
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(conexionAccesor);
                    var content = new StringContent(JsonConvert.SerializeObject(crudAccesorio),
                        Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("postAgregarAccesorio", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        // Limpiar archivo si falla la API
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        TempData["mensaje"] = "Error al guardar en la API";
                        return RedirectToAction("ListaAccesorios");
                    }
                }


                TempData["mensaje"] = "Accesorio Registrado";
                return RedirectToAction("ListaAccesorios");
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = $"Error: {ex.Message}";
                return View(crudAccesorio);
            }
        }

       

        //EDITAR PRODUCTO
        public async Task<IActionResult> ActualizarAccesorio(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("ListaAccesorios");

            CrudAccesorio productoCrear = new CrudAccesorio();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                HttpResponseMessage respons = await client.GetAsync("getBuscarAccesrio/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                productoCrear = JsonConvert.DeserializeObject<CrudAccesorio>(apiResponse);
            }

            var tipos = await ListaTipoAcce();
            ViewBag.tipos = new SelectList(tipos, "idtipo", "tipo");

            var modelos = await ListaModeloAcce();
            ViewBag.modelos = new SelectList(modelos, "idmodelo", "modelo");

            var marcas = await ListaMarcas();
            ViewBag.marcas = new SelectList(marcas, "idmarca", "nombre_marca");

            return View(await Task.Run(() => productoCrear));

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarAccesorio(CrudAccesorio crudAccesorio)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                StringContent content = new StringContent(JsonConvert.SerializeObject(crudAccesorio), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActualizarAccesorio", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("ListaAccesorios");
        }

        // Acción que recibe el ID y elimina desde la API
        public async Task<IActionResult> DeleteAccesorio(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("ListaAccesorios");

            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                HttpResponseMessage response = await client.DeleteAsync("deleteAccesorio/?id=" + id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                mensaje = apiResponse;
            }

            TempData["mensaje"] = mensaje;
            return RedirectToAction("ListaAccesorios");
        }

        //===================================================USUARIOS======================================================
        //===================================================USUARIOS======================================================
        //===================================================USUARIOS======================================================
        //===================================================USUARIOS======================================================
        //===================================================USUARIOS======================================================

        public async Task<IActionResult> ListaUsuarios(int page = 1)
        {
            List<CrudUsuario> temporal = new List<CrudUsuario>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getUsuariosCompleto");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<CrudUsuario>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;

            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * (page - 1)).Take(fila)));
        }

        public async Task<List<Roll>> ListaRolles()
        {
            List<Roll> temporal = new List<Roll>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                HttpResponseMessage response = await client.GetAsync("getRoles");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Roll>>(apiresponse).ToList();
            }
            return temporal;
        }

        public async Task<IActionResult> AgregarUsuario()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AgregarUsuario(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("postAgregaUsuarios", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                TempData["mensaje"] = apiresponse;
            }
            return RedirectToAction("ListaUsuarios");
        }

        public async Task<IActionResult> ActualizarUsuario(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("ListaUsuarios");

            CrudUsuario crudUsuario = new CrudUsuario();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                HttpResponseMessage respons = await client.GetAsync("getBuscarUsuario/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                crudUsuario = JsonConvert.DeserializeObject<CrudUsuario>(apiResponse);
            }

            var rolls = await ListaRolles();
            ViewBag.rolls = new SelectList(rolls, "idroll", "roll");

            var estados = await ListaEstados();
            ViewBag.estados = new SelectList(estados, "idestado", "descripcion");

            return View(await Task.Run(() => crudUsuario));

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(CrudUsuario crudUsuario)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                StringContent content = new StringContent(JsonConvert.SerializeObject(crudUsuario), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActuaUsuariosAdmin", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("ListaUsuarios");
        }


    }
}
