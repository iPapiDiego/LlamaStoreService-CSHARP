using LlamaStoreService.Models.Accesorios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LlamaStoreVista.Controllers
{
    public class AccesoriosController : Controller
    {
        private readonly string conexionService = "https://localhost:44331/api/Accesorio/";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AccesoriosProductos(int page = 1)
        {
            List<Accesorio> temporal = new List<Accesorio>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionService);
                HttpResponseMessage response = await client.GetAsync("getAccesorios");

                if (response.IsSuccessStatusCode) // 👈 Verifica que el API respondió OK
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiresponse))
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<Accesorio>>(apiresponse);
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
            int fila = 7;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
        }
    }
}
