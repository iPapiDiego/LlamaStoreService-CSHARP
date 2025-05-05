using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Auditos;
using LlamaStoreService.Models.Estatus;
using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Products;
using LlamaStoreService.Models.Tickets;
using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

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
            string mensaje = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(conexionProduc);
                StringContent content = new StringContent(JsonConvert.SerializeObject(productoCrear), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("postAgregarCelular", content);
                string apiresponse = await response.Content.ReadAsStringAsync();
                mensaje = apiresponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("ListaProductos");
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
    }
}
