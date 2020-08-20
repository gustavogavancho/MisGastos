using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System.Linq;

namespace MisGastos.BIZ
{
    public class CuentaManager : GenericManager<Cuenta>, ICuentaManager
    {
        public CuentaManager(IGenericRepository<Cuenta> repositorio) : base (repositorio)
        {

        }

        public Cuenta BuscarPorNombre(string nombre)
        {
            return _repositorio.Query(cuenta => cuenta.Nombre == nombre).FirstOrDefault();
        }
    }
}
