using MisGastos.BIZ;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CategoriaDetailViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;

        public CategoriaDetailViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            Title = "Detalle categoria";
        }
    }
}
