using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewModel;
        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ViewModelLocator.HomeViewModel;
        }
    }
}