using MisGastos.UI.Movil.Consumidor.Views;

using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CuentaDetailPage), typeof(CuentaDetailPage));
            Routing.RegisterRoute(nameof(RegistrarIngresoPage), typeof(RegistrarIngresoPage));
            Routing.RegisterRoute(nameof(RegistrarEgresoPage), typeof(RegistrarEgresoPage));
            Routing.RegisterRoute(nameof(CategoriaDetailPage), typeof(CategoriaDetailPage));
        }
    }
}