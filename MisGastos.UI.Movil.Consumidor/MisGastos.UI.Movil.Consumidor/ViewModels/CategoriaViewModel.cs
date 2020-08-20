using MisGastos.BIZ;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;

        public Command AgregarCategoriaCommand { get; }

        public CategoriaViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            Title = "Categoria";
            AgregarCategoriaCommand = new Command(OnAgregarCategoria);
        }

        private async void OnAgregarCategoria(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CategoriaDetailPage));
        }
    }
}
