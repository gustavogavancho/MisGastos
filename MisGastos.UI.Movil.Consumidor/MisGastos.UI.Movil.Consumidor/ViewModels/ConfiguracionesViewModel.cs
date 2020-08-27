using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.UI.Movil.Consumidor.Helpers;
using MisGastos.UI.Movil.Consumidor.Utility;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class ConfiguracionesViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;

        public Command RestaurarValoresPredeterminadosCommand { get; }

        public ConfiguracionesViewModel(FactoryManager factoryManager)
        {
            Title = "Configuraciones";
            _factoryManager = factoryManager;
            RestaurarValoresPredeterminadosCommand = new Command(OnRestaurarValoresPredeterminadosCommnad);
        }

        private async void OnRestaurarValoresPredeterminadosCommnad(object obj)
        {
            if (await Shell.Current.DisplayAlert("Aviso", "¿Esta seguro que desea restablecer valores predeterminados? \nAdvertencia:\nLa aplicación se cerrara y tendra que volver a abrirla de ser si la respuesta.", "Si", "No"))
            {
                SeedData.VaciarCuenta();
                SeedData.SeedCuenta();
                SeedData.VaciarCategoria();
                SeedData.SeedCategoria();
                SeedData.VaciarMovimiento();
                SeedData.VaciarBalance();
                SeedData.SeedBalance();

                MessagingCenter.Send(this, MessageNames.CategoriaChangedMessage, new Categoria());
                MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, new Cuenta());
                MessagingCenter.Send(this, MessageNames.MovimientoChangedMessage, new Cuenta());
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                App.Current.MainPage = new AppShell();

            }
        }
    }
}
