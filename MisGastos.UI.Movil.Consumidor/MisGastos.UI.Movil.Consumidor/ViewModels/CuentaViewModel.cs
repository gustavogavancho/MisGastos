using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Entensions;
using System.Collections.ObjectModel;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CuentaViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;

        public CuentaViewModel(FactoryManager factoryManager)
        {
            Title = "Cuenta";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            GetCuentas();
        }

        public void GetCuentas()
        {
            ObservableCollection<Cuenta> cuentas = _cuentaManager.ObtenerTodo.ToObservableCollection();
        }
    }
}
