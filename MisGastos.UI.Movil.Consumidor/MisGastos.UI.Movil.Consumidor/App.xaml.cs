using MisGastos.BIZ;
using MisGastos.UI.Movil.Consumidor.Helpers;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor
{
    public partial class App : Application
    {
        public static FactoryManager FactoryManager { get; } = new FactoryManager("LiteDB");
        public App()
        {
            InitializeComponent();

            //SeedData.VaciarCuenta();
            SeedData.SeedCuenta();
            //SeedData.VaciarCategoria();
            SeedData.SeedCategoria();
            //SeedData.VaciarMovimiento();
            //SeedData.VaciarBalance();
            SeedData.SeedBalance();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
