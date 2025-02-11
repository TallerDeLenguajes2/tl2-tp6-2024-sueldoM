namespace TP6MVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // "Administrador" o "Cliente"
    }
}
