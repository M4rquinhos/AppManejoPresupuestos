using AppManejoPresupuestos.Models;

namespace AppManejoPresupuestos.Servicios
{
    public interface IRepositorioCategorias
    {
        Task Crear(Categoria categoria);
    }
}
