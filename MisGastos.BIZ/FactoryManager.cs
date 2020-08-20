using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.COMMON.Validadores;
using MisGastos.DAL.Local.LiteDB;

namespace MisGastos.BIZ
{
    public class FactoryManager
    {
        string _origen;

        public FactoryManager(string origen)
        {
            _origen = origen;
        }

        public ICategoriaManager CategoriaManager()
        {
            switch (_origen)
            {
                case "LiteDB":
                    return new CategoriaManager(new GenericRepository<Categoria>(new CategoriaValidator()));
                default:
                    return null;
            }
        }

        public ICuentaManager CuentaManager()
        {
            switch (_origen)
            {
                case "LiteDB":
                    return new CuentaManager(new GenericRepository<Cuenta>(new CuentaValidator()));
                default:
                    return null;
            }
        }

        public IMovimientoManager MovimientoManager()
        {
            switch (_origen)
            {
                case "LiteDB":
                    return new MovimientoManager(new GenericRepository<Movimiento>(new MovimientoValidator()));
                default:
                    return null;
            }
        }

    }
}
