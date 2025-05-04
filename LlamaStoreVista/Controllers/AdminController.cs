using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Tickets;
using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models.Admin;
using LlamaStoreVista.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace LlamaStoreVista.Controllers
{
    public class AdminController : Controller
    {
        private readonly string conexionUser = "https://localhost:44331/api/Usuario/";
        private readonly string conexionProduc = "https://localhost:44331/api/Products/";
        private readonly string conexionAccesor = "https://localhost:44331/api/Accesorio/";
        private readonly string conexionBoleta = "https://localhost:44331/api/Boleta/";

        public async Task<IActionResult> VistaAdmin(int pageUsuarios = 1, int pageProductos = 1)
        {
            var model = new AdminViewModel();

            // 1. Obtener usuarios
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                var response = await client.GetAsync("getUsuarios");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(apiresponse);
                    int fila = 12;
                    int total = usuarios.Count();
                    model.TotalPaginasUsuarios = (int)Math.Ceiling((double)total / fila);
                    model.PaginaActualUsuarios = pageUsuarios;
                    model.Usuarios = usuarios.Skip((pageUsuarios - 1) * fila).Take(fila).ToList();
                }
            }

            // 2. Obtener productos
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                var response = await client.GetAsync("getCelulares");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    var productos = JsonConvert.DeserializeObject<List<Producto>>(apiresponse);
                    int fila = 12;
                    int total = productos.Count();
                    model.TotalPaginasProductos = (int)Math.Ceiling((double)total / fila);
                    model.PaginaActualProductos = pageProductos;
                    model.Productos = productos.Skip((pageProductos - 1) * fila).Take(fila).ToList();
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                var response = await client.GetAsync("getAccesorios");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    var accesorios = JsonConvert.DeserializeObject<List<Accesorio>>(apiresponse);
                    int fila = 12;
                    int total = accesorios.Count();
                    model.TotalPaginasProductos = (int)Math.Ceiling((double)total / fila);
                    model.PaginaActualProductos = pageProductos;
                    model.Accesorios = accesorios.Skip((pageProductos - 1) * fila).Take(fila).ToList();
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionBoleta);
                var response = await client.GetAsync("getBoleta");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    var listaBoletas = JsonConvert.DeserializeObject<List<ListaBoleta>>(apiresponse);
                    int fila = 12;
                    int total = listaBoletas.Count();
                    model.TotalPaginasProductos = (int)Math.Ceiling((double)total / fila);
                    model.PaginaActualProductos = pageProductos;
                    model.boletas = listaBoletas.Skip((pageProductos - 1) * fila).Take(fila).ToList();
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AgregarProducto(Producto producto)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                StringContent content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postAgregarCelular", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        public async Task<IActionResult> ActualizarProducto(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("VistaAdmin");

            Producto producto = new Producto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage respons = await client.GetAsync("getBuscarCelular/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                producto = JsonConvert.DeserializeObject<Producto>(apiResponse);
            }

            return View(await Task.Run(() => producto));

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarProducto(Producto producto)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                StringContent content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("mergeVendedores", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        public async Task<IActionResult> DeleteProducto(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            Producto producto = new Producto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                HttpResponseMessage respons = await client.GetAsync("getBuscarCelular/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                producto = JsonConvert.DeserializeObject<Producto>(apiResponse);
            }
            return View(await Task.Run(() => producto));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProducto(Producto producto)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                //StringContent content = new StringContent(JsonConvert.SerializeObject(vendedor), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync("deleteCelular/?id=" + producto.idcel);
                string apiResponse = await response.Content.ReadAsStringAsync();
                mensaje = apiResponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        //========================================================================================================================

        //USUARIOS
        public async Task<IActionResult> ListaUsuarios(int page = 1)
        {
            List<Usuario> temporal = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                HttpResponseMessage response = await client.GetAsync("getUsuarios");

                if (response.IsSuccessStatusCode) // 👈 Verifica que el API respondió OK
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiresponse))
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<Usuario>>(apiresponse);
                        if (deserialized != null)
                            temporal = deserialized.ToList();
                    }
                }
                else
                {
                    // Opcional: manejar el error
                    Console.WriteLine($"Error API: {response.StatusCode}");
                }
            }
            // Proceso para la paginación
            int fila = 12;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
        }

        [HttpPost]
        public async Task<IActionResult> RegistroUsuario(Usuario usuario)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postAgregaUsuarios", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        public async Task<IActionResult> ActualizarUsuario(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("VistaAdmin");

            Usuario usuario = new Usuario();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                HttpResponseMessage respons = await client.GetAsync("getBuscarUsuario/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
            }
            return View(await Task.Run(() => usuario));
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(Usuario usuario)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActuaUsuariosAdmin", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        //========================================================================================================================

        //ACCESORIO

        [HttpPost]
        public async Task<IActionResult> AgregarAccesorio(Accesorio accesorio)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                StringContent content = new StringContent(JsonConvert.SerializeObject(accesorio), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postAgregarAccesorio", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        public async Task<IActionResult> ActualizarAccesorio(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("VistaAdmin");

            Accesorio accesorio = new Accesorio();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                HttpResponseMessage respons = await client.GetAsync("getBuscarAccesrio/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                accesorio = JsonConvert.DeserializeObject<Accesorio>(apiResponse);
            }

            return View(await Task.Run(() => accesorio));

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarAccesorio(Accesorio accesorio)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                StringContent content = new StringContent(JsonConvert.SerializeObject(accesorio), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActualizarAccesorio", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }

        public async Task<IActionResult> DeleteAccesorio(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            Accesorio accesorio = new Accesorio();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                HttpResponseMessage respons = await client.GetAsync("getBuscarAccesrio/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                accesorio = JsonConvert.DeserializeObject<Accesorio>(apiResponse);
            }
            return View(await Task.Run(() => accesorio));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccesorio(Accesorio accesorio)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionAccesor);
                //StringContent content = new StringContent(JsonConvert.SerializeObject(vendedor), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync("deleteAccesorio/?id=" + accesorio.idacce);
                string apiResponse = await response.Content.ReadAsStringAsync();
                mensaje = apiResponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }
    }
}
