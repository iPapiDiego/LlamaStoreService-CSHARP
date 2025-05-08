using System.ComponentModel.DataAnnotations;

namespace LlamaStoreService.Models.Auths
{
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
