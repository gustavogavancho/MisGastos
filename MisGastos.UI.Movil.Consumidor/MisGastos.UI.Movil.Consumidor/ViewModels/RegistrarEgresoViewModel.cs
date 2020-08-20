using MisGastos.BIZ;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class RegistrarEgresoViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        public RegistrarEgresoViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            Title = "Registrar Egreso";
        }
    }
}
