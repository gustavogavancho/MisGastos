using MisGastos.UI.Movil.Consumidor.ViewModels;

namespace MisGastos.UI.Movil.Consumidor.Utility
{
    public static class ViewModelLocator
    {
        public static CuentaViewModel CuentaViewModel { get; set; } =
            new CuentaViewModel(App.FactoryManager);

        public static CuentaDetailViewModel CuentaDetailViewModel { get; set; } =
            new CuentaDetailViewModel(App.FactoryManager);

        public static HomeViewModel HomeViewModel { get; set; } =
            new HomeViewModel(App.FactoryManager);

        public static CategoriaViewModel CategoriaViewModel { get; set; } =
            new CategoriaViewModel(App.FactoryManager);

        public static RegistrarIngresoViewModel RegistrarIngresoViewModel { get; set; } =
            new RegistrarIngresoViewModel(App.FactoryManager);

        public static RegistrarEgresoViewModel RegistrarEgresoViewModel { get; set; } =
            new RegistrarEgresoViewModel(App.FactoryManager);

        public static CategoriaDetailViewModel CategoriaDetailViewModel { get; set; } =
            new CategoriaDetailViewModel(App.FactoryManager);

        public static ConfiguracionesViewModel ConfiguracionesViewModel { get; set; } =
            new ConfiguracionesViewModel(App.FactoryManager);
    }
}
