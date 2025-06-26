using LlamaStoreVista.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LlamaStoreVista.Controllers
{
    public class ProductosController : Controller
    {
        private readonly string conexionService = "https://localhost:44331/api/Products/";


        public async Task<IActionResult> Index()
        {
            List<Producto> productos = new List<Producto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage response = await client.GetAsync("getCelulares"); // Asegúrate que sea el endpoint correcto

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(apiResponse);
                }
                else
                {
                    Console.WriteLine($"Error al llamar a la API: {response.StatusCode}");
                }
            }

            return View(productos);
        }

        //LISTA DE VENDEDORES CON NUMERACION
        public async Task<IActionResult> IphonesProductos(int page = 1)
        {
            List<Producto> temporal = new List<Producto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage response = await client.GetAsync("getCelulares");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiresponse))
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<Producto>>(apiresponse);
                        if (deserialized != null)
                        {
                            // 🔍 FILTRO: Solo iPhone (iOS)
                            temporal = deserialized
                                .Where(p => p.tipo_sistema != null &&
                                            p.tipo_sistema.ToLower().Contains("ios"))
                                .ToList();
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error API: {response.StatusCode}");
                }
            }

            // Paginación
            int fila = 12;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
        }




        public async Task<IActionResult> AndroidProductos(int page = 1)
        {
            List<Producto> temporal = new List<Producto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage response = await client.GetAsync("getCelulares");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiresponse))
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<Producto>>(apiresponse);
                        if (deserialized != null)
                        {
                            // 🔍 FILTRO: Solo Android
                            temporal = deserialized
                                .Where(p => p.tipo_sistema != null && p.tipo_sistema.ToLower().Contains("android"))
                                .ToList();
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error API: {response.StatusCode}");
                }
            }

            // Paginación
            int fila = 12;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
        }

    }
}
