using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models;
using LlamaStoreVista.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace LlamaStoreVista.Controllers
{
    public class AdminController : Controller
    {
        private readonly string conexionService = "https://localhost:44331/api/Usuario/";

        public IActionResult VistaAdmin()
        {
            return View();
        }

        //Productos
        public async Task<IActionResult> ListaProductos(int page = 1)
        {
            List<Producto> temporal = new List<Producto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage response = await client.GetAsync("getCelulares");

                if (response.IsSuccessStatusCode) // 👈 Verifica que el API respondió OK
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiresponse))
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<Producto>>(apiresponse);
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
        public async Task<IActionResult> AgregarProducto(Producto producto)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
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
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage respons = await client.GetAsync("getBuscarCelular/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                producto = JsonConvert.DeserializeObject<Producto>(apiResponse);
            }

            return View(await Task.Run(() => producto));

        }

        [HttpPost]
        public async Task<IActionResult> EditProducto(Producto producto)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                StringContent content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("mergeVendedores", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }



        //===================================================================

        //USUARIOS
        public async Task<IActionResult> ListaUsuarios(int page = 1)
        {
            List<Usuario> temporal = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
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
                client.BaseAddress = new Uri(conexionService);
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
                client.BaseAddress = new Uri(conexionService);
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
                client.BaseAddress = new Uri(conexionService);
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActuaUsuariosAdmin", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("VistaAdmin");
        }
    }
}
