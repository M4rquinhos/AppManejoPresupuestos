using AppManejoPresupuestos.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppManejoPresupuestos.Models
{
    public class TipoCuenta
    {
        public int IdTipoCuenta { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta", controller: "TiposCuentas")]
        public string Nombre { get; set; }

        public int UsuarioId { get; set; }

        public int Orden { get; set; }
    }
}
