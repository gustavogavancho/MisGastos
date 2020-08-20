using MisGastos.BIZ;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor
{
    public partial class App : Application
    {
        public static FactoryManager FactoryManager { get; } = new FactoryManager("LiteDB");
        public App()
        {
            InitializeComponent();

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
