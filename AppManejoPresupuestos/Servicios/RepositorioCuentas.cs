using AppManejoPresupuestos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppManejoPresupuestos.Servicios
{
    public class RepositorioCuentas : IRepositorioCuentas
    {
        private readonly string _connectionString;

        public RepositorioCuentas(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(
                                        @"INSERT INTO Cuentas (Nombre, TipoCuentaId, Balance, Descripcion)
                                          VALUES (@nombre, @tipoCuentaId, @balance, @descripcion);
                                          SELECT SCOPE_IDENTITY();", cuenta);
            cuenta.IdCuenta = id;
        }

        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Cuenta>(
                                               @"SELECT C.IdCuenta, C.Nombre, Balance, TC.Nombre AS TipoCuenta
                                                 FROM Cuentas AS C
                                                 INNER JOIN TiposCuentas AS TC
                                                 ON TC.IdTipoCuenta = C.TipoCuentaId
                                                 WHERE TC.UsuarioId = @usuarioId
                                                 ORDER BY TC.Orden", new { usuarioId });
        }

        public async Task<Cuenta> ObtenerPorId(int idCuenta, int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Cuenta>(
                                               @"SELECT C.IdCuenta, C.Nombre, Balance, C.Descripcion, C.TipoCuentaId
                                                 FROM Cuentas AS C
                                                 INNER JOIN TiposCuentas AS TC
                                                 ON TC.IdTipoCuenta = C.TipoCuentaId
                                                 WHERE TC.UsuarioId = @usuarioId AND C.IdCuenta = @idCuenta;",
                                                 new { idCuenta, usuarioId });
        }

        public async Task Actualizar(CuentaCreacionViewModel cuenta)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                                    @"UPDATE Cuentas
                                      SET 
                                      Nombre = @nombre, Balance = @balance, Descripcion = @descripcion,
                                      TipoCuentaId = @tipoCuentaId
                                      WHERE IdCuenta = @idCuenta;", cuenta);
        }
    }
}
