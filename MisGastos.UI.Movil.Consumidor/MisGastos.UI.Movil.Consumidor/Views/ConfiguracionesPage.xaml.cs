using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfiguracionesPage : ContentPage
    {
        ConfiguracionesViewModel _viewModel;
        public ConfiguracionesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ViewModelLocator.ConfiguracionesViewModel;
        }
    }
}