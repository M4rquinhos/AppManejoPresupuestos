using AppManejoPresupuestos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppManejoPresupuestos.Servicios
{
    public class RepositorioTipoCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;

        public RepositorioTipoCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>
                                                    ("spTiposCuentasInsertar",
                                                    new { usuarioId = tipoCuenta.UsuarioId, nombre = tipoCuenta.Nombre },
                                                    commandType: System.Data.CommandType.StoredProcedure) ;
            tipoCuenta.IdTipoCuenta = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                                                                (@"SELECT 1
                                                                   FROM TiposCuentas 
                                                                   WHERE Nombre = @nombre AND UsuarioId = @usuarioId;", new { nombre, usuarioId });
            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>
                                                (@"SELECT IdTipoCuenta, Nombre, Orden 
                                                   FROM TiposCuentas 
                                                   WHERE UsuarioId = @usuarioId
                                                   ORDER BY Orden;", new { usuarioId });
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync
                                        (@"UPDATE TiposCuentas 
                                           SET Nombre = @nombre 
                                           WHERE IdTipoCuenta = @IdTipoCuenta;", tipoCuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int tipoCuentaId, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>
                                                                (@"SELECT IdTipoCuenta, Nombre, Orden 
                                                                   FROM TiposCuentas 
                                                                   WHERE IdTipoCuenta = @tipoCuentaId AND UsuarioId = @usuarioId;", new { tipoCuentaId, usuarioId });
        }

        public async Task Eliminar(int tipoCuentaId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE TiposCuentas WHERE IdTipoCuenta = @tipoCuentaId", new { tipoCuentaId });
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrden)
        {
            var query = "UPDATE TiposCuentas SET Orden = @orden Where IdTipoCuenta = @idTipoCuenta;";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrden);
        }
    }
}
