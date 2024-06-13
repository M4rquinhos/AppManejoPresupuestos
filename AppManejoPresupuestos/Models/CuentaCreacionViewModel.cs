using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppManejoPresupuestos.Models
{
    public class CuentaCreacionViewModel :  Cuenta
    {
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }
    }
}
