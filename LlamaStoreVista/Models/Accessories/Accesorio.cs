namespace LlamaStoreService.Models.Accesorios
{
    public class Accesorio
    {
        public int idacce { get; set; }
        public string tipo { get; set; }
        public string nombre_marca { get; set; }
        public string modelo { get; set; }
        public decimal precio { get; set; }
        public decimal precio_oferta { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
