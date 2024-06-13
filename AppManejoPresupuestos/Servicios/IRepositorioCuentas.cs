using AppManejoPresupuestos.Models;

namespace AppManejoPresupuestos.Servicios
{
    public interface IRepositorioCuentas
    {
        Task Actualizar(CuentaCreacionViewModel cuenta);
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
        Task<Cuenta> ObtenerPorId(int idCuenta, int usuarioId);
    }
}
