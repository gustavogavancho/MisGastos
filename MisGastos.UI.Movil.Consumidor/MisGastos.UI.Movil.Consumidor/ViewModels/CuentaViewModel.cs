﻿using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Entensions;
using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CuentaViewModel : BaseViewModel
    {
        private ObservableCollection<Cuenta> _cuentas;

        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;

        public ObservableCollection<Cuenta> Cuentas
        {
            get => _cuentas;
            set => SetProperty(ref _cuentas, value);
        }

        public Command AgregarCuentaCommand { get; }
        public Command CuentaSelectedCommand { get; }

        public CuentaViewModel(FactoryManager factoryManager)
        {
            Title = "Cuenta";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            AgregarCuentaCommand = new Command(OnAgregarCuenta);
            CuentaSelectedCommand = new Command<Cuenta>(OnCuentaSelected);
            ActualizarDatos();

            MessagingCenter.Subscribe<CuentaDetailViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaChanged);
        }

        private async void OnCuentaSelected(Cuenta cuenta)
        {
            await Shell.Current.GoToAsync($"{nameof(CuentaDetailPage)}?{nameof(CuentaDetailViewModel.CuentaId)}={cuenta.Id}");
        }

        private void OnCuentaChanged(CuentaDetailViewModel sender, Cuenta cuenta)
        {
            Cuentas = new ObservableCollection<Cuenta>(_cuentaManager.ObtenerTodo.ToObservableCollection());
        }

        private void ActualizarDatos()
        {
            Cuentas = new ObservableCollection<Cuenta>(_cuentaManager.ObtenerTodo.ToObservableCollection());
        }

        private async void OnAgregarCuenta(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CuentaDetailPage));
            //await Shell.Current.Navigation.PushModalAsync(new CuentaDetailPage());
        }
    }
}
