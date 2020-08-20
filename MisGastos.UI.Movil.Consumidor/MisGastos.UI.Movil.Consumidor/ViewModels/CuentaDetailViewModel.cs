using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    [QueryProperty(nameof(CuentaId), nameof(CuentaId))]
    public class CuentaDetailViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;

        private Cuenta _cuenta;
        private string _cuentaId;

        public Cuenta Cuenta
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public string CuentaId
        {
            //get => _cuentaId;
            //set => SetProperty(ref _cuentaId, value);
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

        public Command GuardarCuentaCommand { get; }
        public Command EliminarCuentaCommand { get; }

        public CuentaDetailViewModel(FactoryManager factoryManager)
        {
            Title = "Detalle Cuenta";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            GuardarCuentaCommand = new Command(OnGuardarCuenta);
            EliminarCuentaCommand = new Command(OnEliminarCuenta);
            ActualizarDatos();
        }

        private void LoadCuentaId(string cuentaId)
        {
            try
            {
                Cuenta = _cuentaManager.SearchById(cuentaId);
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
            _cuentaManager.Eliminar(CuentaId);

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }

        private async void OnGuardarCuenta(object obj)
        {
            if (string.IsNullOrEmpty(Cuenta.Id))
            {
                _cuentaManager.Insertar(Cuenta);
            }
            else
            {
                _cuentaManager.Actualizar(Cuenta);
            }

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }
    }
}
