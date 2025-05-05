using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Auditos;
using LlamaStoreService.Models.Tickets;
using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getAuditoria");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Auditoria>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
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
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
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
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
        }

        public async Task<IActionResult> ListaUsuarios(int page = 1)
        {
            List<Usuario> temporal = new List<Usuario>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionUser);
                //LLAMAR SIEMPRE AL "getVendedores" DE LOS SERVICIOS
                HttpResponseMessage response = await client.GetAsync("getUsuarios");
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Usuario>>(apiresponse).ToList();
            }

            //PROCESO PARA LA PAGINACION
            int fila = 5;
            int count = temporal.Count();
            int pages = count % fila == 0 ? count / fila : count / fila + 1;
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
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
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
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
            page = page - 1;
            ViewBag.page = page;
            ViewBag.pages = pages;

            return View(await Task.Run(() => temporal.Skip(fila * page).Take(fila)));
        }

        

    }
}
