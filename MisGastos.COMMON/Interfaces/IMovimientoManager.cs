using MisGastos.COMMON.Entidades;
using System;
using System.Collections.Generic;

namespace MisGastos.COMMON.Interfaces
{
    public interface IMovimientoManager : IGenericManager<Movimiento>
    {
        IEnumerable<Movimiento> BuscarPorCategoria(string idCategoria);

        IEnumerable<Movimiento> BuscarPorFecha(DateTime fecha);
    }
}
