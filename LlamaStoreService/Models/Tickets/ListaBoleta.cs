namespace LlamaStoreService.Models.Tickets
{
    public class ListaBoleta
    {
        public String num_bol { get; set; }
        public DateTime fch_bol { get; set; }
        public int cod_cliente { get; set; }
        public string nombre { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public string metodo_pago { get; set; }
        public int estado_pago { get; set; }
        public int iddetalle { get; set; }
        public string tipo_producto { get; set; }
        public string id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal descuento { get; set; }
        public decimal precio_final { get; set; }
    }
}
