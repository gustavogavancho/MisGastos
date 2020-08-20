using MisGastos.BIZ;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;

        public Command RegistrarIngresoCommnad { get; } 
        public Command RegistrarEgresoCommand { get; }

        public HomeViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            Title = "Inicio";
            RegistrarIngresoCommnad = new Command(OnRegistrarIngreso);
            RegistrarEgresoCommand = new Command(OnRegistrarEgreso);
        }

        private async void OnRegistrarEgreso(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegistrarEgresoPage));
        }

        private async void OnRegistrarIngreso(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegistrarIngresoPage));
        }
    }
}
