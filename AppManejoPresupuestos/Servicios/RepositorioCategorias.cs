using AppManejoPresupuestos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppManejoPresupuestos.Servicios
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly string _connectionString;

        public RepositorioCategorias(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(
                                                    @"INSERT INTO Categorias (Nombre, TipoOperacionId, UsuarioId)
                                                      VALUES (@nombre, @tipoOperacionId, @usuarioId);
                                                      SELECT SCOPE_IDENTITY();", categoria);
            categoria.Id = id;
        }
    }
}
