using LlamaStoreService.Models.Auths;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LlamaStoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("sp_login", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Correo", request.Correo);
                        command.Parameters.AddWithValue("@Clave", request.Clave);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                var usuario = new Usuario
                                {
                                    Codigo = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Correo = reader.GetString(3),
                                    Fnacim = reader.GetDateTime(4),
                                    Idrol = reader.GetInt32(5),
                                    Estado = reader.GetInt32(6)
                                };

                                var token = GenerateJwtToken(usuario);
                                return Ok(new LoginResponse { Token = token, Usuario = usuario });
                            }
                        }
                    }
                }
                return Unauthorized("Credenciales incorrectas");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Codigo.ToString()),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.Idrol == 1 ? "Admin" : "Cliente"),
            new Claim("NombreCompleto", $"{usuario.Nombre} {usuario.Apellido}")
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
