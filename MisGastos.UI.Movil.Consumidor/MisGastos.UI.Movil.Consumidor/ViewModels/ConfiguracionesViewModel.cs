using MisGastos.COMMON.Entidades;
using MisGastos.UI.Movil.Consumidor.Helpers;
using MisGastos.UI.Movil.Consumidor.Utility;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class ConfiguracionesViewModel : BaseViewModel
    {
        public Command RestaurarValoresPredeterminadosCommand { get; }

        public ConfiguracionesViewModel()
        {
            Title = "Configuraciones";
            RestaurarValoresPredeterminadosCommand = new Command(OnRestaurarValoresPredeterminadosCommnad);
        }

        private async void OnRestaurarValoresPredeterminadosCommnad(object obj)
        {
            if (await Shell.Current.DisplayAlert("Aviso", "¿Esta seguro que querer restablecer valores predeterminados?\nLa aplicación se cerrará de ser si el caso.", "Si", "No"))
            {
                SeedData.VaciarCuenta();
                SeedData.SeedCuenta();
                SeedData.VaciarCategoria();
                SeedData.SeedCategoria();
                SeedData.VaciarMovimiento();
                SeedData.VaciarBalance();
                SeedData.SeedBalance();

                MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, new Cuenta());
                MessagingCenter.Send(this, MessageNames.CategoriaChangedMessage, new Categoria());
                MessagingCenter.Send(this, MessageNames.MovimientoChangedMessage, new Movimiento());
                MessagingCenter.Send(this, MessageNames.BalanceChangedMessage, new Balance());

                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                //System.Diagnostics.Process.GetCurrentProcess().Close();
                //System.Diagnostics.Process.GetCurrentProcess().Kill();
                //System.Environment.Exit(0);
            }
        }
    }
}
