﻿using AppManejoPresupuestos.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace AppManejoPresupuestos.Models
{
    public class Cuenta
    {
        public int IdCuenta { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Display(Name = "Tipo Cuenta")]
        public int TipoCuentaId { get; set; }

        public decimal Balance { get; set; }

        [StringLength(maximumLength: 1000)]
        public string Descripcion { get; set; }

        public string TipoCuenta { get; set; }
    }
}
