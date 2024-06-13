using AppManejoPresupuestos.Models;

namespace AppManejoPresupuestos.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Crear(TipoCuenta tipoCuenta);
        Task Eliminar(int id);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int tipoCuentaId, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrden);
    }
}
