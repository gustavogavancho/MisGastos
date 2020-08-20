using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriaPage : ContentPage
    {
        CategoriaViewModel _viewModel;

        public CategoriaPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ViewModelLocator.CategoriaViewModel;
        }
    }
}