using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Auditos;
using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Products;
using LlamaStoreService.Models.Tickets;
using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LlamaStoreVista.Models.Admin
{
    public class AdminViewModel
    {
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<Accesorio> Accesorios { get; set; } = new List<Accesorio>();
        public List<ListaBoleta> boletas { get; set; } = new List<ListaBoleta>();
        public List<Auditoria> Auditorias { get; set; } = new List<Auditoria>();


        public int PaginaActualUsuarios { get; set; }
        public int TotalPaginasUsuarios { get; set; }

        public int PaginaActualProductos { get; set; }
        public int TotalPaginasProductos { get; set; }

        public int PaginaActualAccesorio { get; set; }
        public int TotalPaginasAccesorio { get; set; }

        public Usuario NuevoUsuario { get; set; } = new Usuario();
        public ProductoCrear NuevoProducto { get; set; } = new ProductoCrear();
        public Accesorio NuevoAccesorio { get; set; } = new Accesorio();

        public List<SelectListItem> ListaSistemas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaGamas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaMarcas { get; set; } = new List<SelectListItem>();


        public List<SelectListItem> ListaEstados { get; set; } = new List<SelectListItem>();
    }
}
