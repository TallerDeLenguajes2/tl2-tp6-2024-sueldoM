using TP6MVC.Models;

namespace TP6MVC.Repositories
{
    public interface IUsuariosRepository
    {
        Usuario? ObtenerPorUsuario(string usuario);
    }

    public class UsuariosRepository : IUsuariosRepository
    {
        private List<Usuario> usuarios = new()
        {
            new Usuario { Id = 1, Nombre = "Admin", UsuarioNombre = "admin", Contraseña = "1234", Rol = "Administrador" },
            new Usuario { Id = 2, Nombre = "Cliente", UsuarioNombre = "cliente", Contraseña = "1234", Rol = "Cliente" }
        };

        public Usuario? ObtenerPorUsuario(string usuario)
        {
            return usuarios.FirstOrDefault(u => u.UsuarioNombre == usuario);
        }
    }
}
