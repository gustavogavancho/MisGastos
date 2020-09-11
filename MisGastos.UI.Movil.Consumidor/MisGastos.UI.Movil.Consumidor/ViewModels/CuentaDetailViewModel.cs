using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Helpers;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    [QueryProperty(nameof(CuentaId), nameof(CuentaId))]
    public class CuentaDetailViewModel : BaseViewModel
    {
        ICuentaManager _cuentaManager;

        private Cuenta _cuenta;
        private string _cuentaId;
        private ObservableCollection<string> _imageList;

        public Cuenta Cuenta
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public string CuentaId
        {
            get
            {
                return _cuentaId;
            }
            set
            {
                _cuentaId = value;
                LoadCuentaId(value);
            }
        }

        public ObservableCollection<string> ImageList
        {
            get => _imageList;
            set => SetProperty(ref _imageList, value);
        }

        public Command GuardarCuentaCommand { get; }
        public Command EliminarCuentaCommand { get; }
        public Command RegresarCommand { get;  }
        public Command OnApperaringCommand { get; }
        public Command OnDisappearingCommand { get; }

        public CuentaDetailViewModel(FactoryManager factoryManager)
        {
            Title = "Detalle Cuenta";
            _cuentaManager = factoryManager.CuentaManager();
            GuardarCuentaCommand = new Command(OnGuardarCuenta);
            EliminarCuentaCommand = new Command(OnEliminarCuenta);
            RegresarCommand = new Command(OnRegresar);
            OnApperaringCommand = new Command(OnApperaring);
            OnDisappearingCommand = new Command(OnDisappearing);
            ImageList = ImagePath.ImagesUrlPath;
            ActualizarDatos();
        }

        private void OnApperaring(object obj)
        {
            Shell.Current.Navigating += Current_Navigating;
        }

        private void OnDisappearing()
        {
            Shell.Current.Navigating -= Current_Navigating;
            ActualizarDatos();
        }

        private async void Current_Navigating(object sender,
                                ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                e.Cancel();
                await GoBack();
            }
        }

        private async Task GoBack()
        {
            Shell.Current.Navigating -= Current_Navigating;
            await Shell.Current.GoToAsync("..", true);
        }

        private async void OnRegresar(object obj)
        {
            ActualizarDatos();

            await Shell.Current.Navigation.PopAsync();
        }

        private void LoadCuentaId(string cuentaId)
        {
            try
            {
                Cuenta = _cuentaManager.SearchById(cuentaId);
                int index = ImageList.IndexOf(Cuenta.ImageUrl);
                Cuenta.ImageUrl = null;
                Cuenta.ImageUrl = ImageList[index];
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private void ActualizarDatos()
        {
            Cuenta = new Cuenta();
        }

        private async void OnEliminarCuenta(object obj)
        {
            if (await Shell.Current.DisplayAlert("Aviso", $"¿Estas seguro de querer eliminar la cuenta \"{Cuenta.Nombre}\"?", "Si", "No"))
            {
                _cuentaManager.Eliminar(CuentaId);

                MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

                await Shell.Current.Navigation.PopAsync();

                ActualizarDatos();
            }
        }

        private async void OnGuardarCuenta(object obj)
        {
            if (string.IsNullOrEmpty(Cuenta.Id))
            {
                Cuenta.Balance = 0.0M;
                if (_cuentaManager.Insertar(Cuenta) is null)
                {
                    await Shell.Current.DisplayAlert("Advertencia", _cuentaManager.Error, "Aceptar");
                    return;
                }
            }
            else
            {
                if (_cuentaManager.Actualizar(Cuenta) is null)
                {
                    await Shell.Current.DisplayAlert("Advertencia", _cuentaManager.Error, "Aceptar");
                    return;
                }
            }

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }
    }
}