using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LlamaStoreService.Models.Auths;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;

namespace LlamaStoreVista.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.PostAsJsonAsync("auth/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginResponse.Usuario.Codigo.ToString()),
                new Claim(ClaimTypes.Email, loginResponse.Usuario.Correo),
                new Claim(ClaimTypes.Role, loginResponse.Usuario.Idrol == 1 ? "Admin" : "Cliente"),
                new Claim("Token", loginResponse.Token),
                new Claim("NombreCompleto", $"{loginResponse.Usuario.Nombre} {loginResponse.Usuario.Apellido}")
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Productos");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Credenciales incorrectas
                    ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Puedes leer el mensaje de error específico de la API si lo devuelve
                    var errorResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    if (errorResponse != null && errorResponse.ContainsKey("message"))
                    {
                        ModelState.AddModelError(string.Empty, errorResponse["message"]);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error en la solicitud. Por favor, verifica tus datos.");
                    }
                }
                else
                {
                    // Otros errores
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo nuevamente.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Productos");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }



        public class AuthorizeRolesAttribute : TypeFilterAttribute
        {
            public AuthorizeRolesAttribute(params string[] roles) : base(typeof(AuthorizeRolesFilter))
            {
                Arguments = new object[] { roles };
            }
        }

        public class AuthorizeRolesFilter : IAuthorizationFilter
        {
            private readonly string[] _roles;

            public AuthorizeRolesFilter(string[] roles)
            {
                _roles = roles;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;

                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToActionResult("Login", "Auth", null);
                    return;
                }

                var hasRole = _roles.Any(role => user.IsInRole(role));
                if (!hasRole)
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
                }
            }
        }

        [AuthorizeRoles("Admin")]
        public class AdminController : Controller
        {
            public IActionResult Dashboard()
            {
                return View();
            }
        }

        [AuthorizeRoles("Cliente")]
        public class ClienteController : Controller
        {
            public IActionResult Perfil()
            {
                return View();
            }
        }

        public class ApiService
        {
            private readonly IHttpClientFactory _httpClientFactory;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            {
                _httpClientFactory = httpClientFactory;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<T> GetAsync<T>(string endpoint)
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }

            // Similar para PostAsync, PutAsync, etc.
        }
    }
}
