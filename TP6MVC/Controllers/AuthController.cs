using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            // Supongamos que tienes un método que valida las credenciales
            bool accesoValido = ValidarCredenciales(request.Usuario, request.Clave);

            if (accesoValido)
            {
                _logger.LogInformation($"El usuario {request.Usuario} ingresó correctamente.");
                return Ok(new { mensaje = "Acceso exitoso" });
            }
            else
            {
                _logger.LogWarning($"Intento de acceso inválido - Usuario: {request.Usuario}, Clave ingresada: {request.Clave}");
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error en el login: {ex}");
            return StatusCode(500, new { mensaje = "Error interno del servidor" });
        }
    }

    private bool ValidarCredenciales(string usuario, string clave)
    {
        // Implementación ficticia de autenticación
        return usuario == "admin" && clave == "1234";
    }
}
