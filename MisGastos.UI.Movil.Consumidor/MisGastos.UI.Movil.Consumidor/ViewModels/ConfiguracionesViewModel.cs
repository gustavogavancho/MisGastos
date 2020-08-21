using MisGastos.BIZ;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class ConfiguracionesViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;

        public ConfiguracionesViewModel(FactoryManager factoryManager)
        {
            Title = "Configuraciones";
            _factoryManager = factoryManager;
        }
    }
}
