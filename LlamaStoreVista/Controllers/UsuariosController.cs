using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace LlamaStoreVista.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly string conexionService = "https://localhost:44331/api/Usuario/";

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

        public IActionResult login()
        {
            return View();
        }

        public IActionResult ActualizarUsuario()
        {

            //SI VAMOS A USAR ESTE CODIGO NO OLVIDAR COLOCAR asyng TASK<IActionResult>

            /*
            public async Task<IActionResult> ActualizarUsuario(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            Vendedor vendedor = new Vendedor();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage respons = await client.GetAsync("getBuscarVendedor/?id=" + id);
                string apiResponse = await respons.Content.ReadAsStringAsync();
                vendedor = JsonConvert.DeserializeObject<Vendedor>(apiResponse);
            }

            var paises = await ListaPaises();
            ViewBag.paises = new SelectList(paises, "idPais", "pais", vendedor?.idPais);
            return View(await Task.Run(() => vendedor));

        }
            }*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(Usuario usuario)
        {
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postActuaUsuariosCliente", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("ActualizarUsuario");
        }

        public IActionResult registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> registro(Usuario usuario)
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
            return RedirectToAction("login");
        }
    }
}
