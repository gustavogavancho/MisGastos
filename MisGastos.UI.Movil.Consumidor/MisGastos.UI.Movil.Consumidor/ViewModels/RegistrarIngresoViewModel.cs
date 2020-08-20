using MisGastos.BIZ;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class RegistrarIngresoViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;

        public RegistrarIngresoViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            Title = "Registrar Ingreso";
        }
    }
}
