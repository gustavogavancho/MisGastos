using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarIngresoPage : ContentPage
    {
        RegistrarIngresoViewModel _viewModel;

        public RegistrarIngresoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ViewModelLocator.RegistrarIngresoViewModel;
        }
    }
}