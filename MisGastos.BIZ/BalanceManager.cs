using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System.Linq;

namespace MisGastos.BIZ
{
    public class BalanceManager : GenericManager<Balance>, IBalanceManager
    {
        public BalanceManager(IGenericRepository<Balance> repositorio) : base(repositorio)
        {

        }

        public Balance BuscarBalancePorId(string id)
        {
            return _repositorio.Query(balance => balance.Id == id).FirstOrDefault();
        }
    }
}
