namespace LlamaStoreVista.Models.Carrito
{
    public class Carrito
    {
        public int idCarrito { get; set; }
        public int codigo { get; set; }
        public string idcel { get; set; }
        public string tipo { get; set; }
        public string modelo { get; set; }
        public int cantidad { get; set; }

        public decimal precio_oferta { get; set; }
    }
}
