namespace LlamaStoreVista.Models.Product
{
    public class Producto
    {
        public string idcel { get; set; }
        public string nombre_marca { get; set; }
        public string modelo { get; set; }
        public string tipo_sistema { get; set; }
        public decimal precio { get; set; }
        public decimal precio_oferta { get; set; }
        public string tipo_gama { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        
    }
}
