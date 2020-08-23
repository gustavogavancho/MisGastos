using System;

namespace MisGastos.COMMON.Entidades
{
    public class Movimiento : BaseDTO
    {
        public string IdCuenta { get; set; }
        public string IdCategoria { get; set; }
        public string Monto { get; set; }
        public string ImageUrl { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
