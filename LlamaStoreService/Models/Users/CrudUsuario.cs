namespace LlamaStoreService.Models.Users
{
    public class CrudUsuario
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public DateTime fnacim { get; set; }
        public string clave { get; set; }
        public int idrol { get; set; }
        public int estado { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime last_login { get; set; }
        public int intentos_login { get; set; }
    }
}
