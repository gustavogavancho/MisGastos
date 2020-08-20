using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CuentaDetailPage : ContentPage
    {
        CuentaDetailViewModel _viewModel;

        public CuentaDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ViewModelLocator.CuentaDetailViewModel;
        }
    }
}