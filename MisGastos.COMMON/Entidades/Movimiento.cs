using System;

namespace MisGastos.COMMON.Entidades
{
    public class Movimiento : BaseDTO
    {
        public string IdCategoria { get; set; }
        public string Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
