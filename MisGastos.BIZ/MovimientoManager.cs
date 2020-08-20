using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.BIZ
{
    public class MovimientoManager : GenericManager<Movimiento>, IMovimientoManager
    {
        public MovimientoManager(IGenericRepository<Movimiento> repositorio) : base (repositorio)
        {

        }

        public IEnumerable<Movimiento> BuscarPorCategoria(string idCategoria)
        {
            return _repositorio.Query(movimiento => movimiento.IdCategoria == idCategoria);
        }

        public IEnumerable<Movimiento> BuscarPorFecha(DateTime fecha)
        {
            return _repositorio.Query(movimiento => movimiento.Fecha == fecha);
        }
    }
}
