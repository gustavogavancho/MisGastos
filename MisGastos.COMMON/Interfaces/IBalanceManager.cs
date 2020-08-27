using MisGastos.COMMON.Entidades;

namespace MisGastos.COMMON.Interfaces
{
    public interface IBalanceManager : IGenericManager<Balance>
    {
        Balance BuscarBalancePorId(string id);
    }
}
