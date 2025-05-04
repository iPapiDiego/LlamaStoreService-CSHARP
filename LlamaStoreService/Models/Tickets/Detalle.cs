namespace LlamaStoreService.Models.Tickets
{
    public class Detalle
    {
        public int iddetalle { get; set; }
        public string num_bol { get; set; }
        public string tipo_producto { get; set; }
        public string id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal descuento { get; set; }
        public decimal precio_final { get; set; }
    }
}
