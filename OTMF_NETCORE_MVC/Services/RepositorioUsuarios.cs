using Dapper;
using OTMF_NETCORE_MVC.Models;
using System.Data.SqlClient;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IRepositorioUsuarios
    {
        Task<int> CrearUsuario(Usuario user);
        Task<Usuario> BuscarUsuarioPorEmail(string normalizedEmail);
    }
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsuarios (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> CrearUsuario (Usuario usuario)
        {
            //usuario.EmailNormalizado = usuario.Email.ToUpper();
            using var connection = new SqlConnection(connectionString);
            var IdUsuarios = await connection.QueryFirstOrDefaultAsync<int>("INSERT INTO Usuarios (Email , EmailNormalizado , PasswordHash) VALUES (@Email , @EmailNormalizado , @PasswordHash)", usuario);
            Console.WriteLine(IdUsuarios);
            return IdUsuarios;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var con = new SqlConnection(connectionString);
            return await con.QuerySingleOrDefaultAsync<Usuario>
                ("SELECT * FROM Usuarios where EmailNormalizado = @emailNormalizado",
                new { emailNormalizado });
        }
    }
}
