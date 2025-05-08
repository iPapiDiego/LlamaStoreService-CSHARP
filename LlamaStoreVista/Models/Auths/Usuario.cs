namespace LlamaStoreService.Models.Auths
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateTime Fnacim { get; set; }
        public int Idrol { get; set; }
        public int Estado { get; set; }
    }
}
