using AppManejoPresupuestos.Models;
using AppManejoPresupuestos.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace AppManejoPresupuestos.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IRepositorioCategorias _repositorioCategorias;
        private readonly IServicioUsuarios _servicioUsuarios;

        public CategoriasController(
            IRepositorioCategorias repositorioCategorias,
            IServicioUsuarios servicioUsuarios
            )
        {
            _repositorioCategorias = repositorioCategorias;
            _servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Categoria categoria)
        {

            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
            categoria.UsuarioId = usuarioId;
            await _repositorioCategorias.Crear(categoria);
            return RedirectToAction("Index");
        }
    }
}
