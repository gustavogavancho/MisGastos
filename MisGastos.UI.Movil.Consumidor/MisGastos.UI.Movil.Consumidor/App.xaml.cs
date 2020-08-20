using MisGastos.BIZ;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor
{
    public partial class App : Application
    {
        public static FactoryManager _factory;
        public App()
        {
            InitializeComponent();

            _factory = new FactoryManager("LiteDB");

            //MainPage = new MainPage();
            MainPage = new MasterDetailPageView();
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
