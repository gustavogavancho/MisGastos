using MisGastos.COMMON.Entidades;

namespace MisGastos.COMMON.Interfaces
{
    public interface ICuentaManager : IGenericManager<Cuenta>
    {
        Cuenta BuscarPorNombre(string nombre);
    }
}
