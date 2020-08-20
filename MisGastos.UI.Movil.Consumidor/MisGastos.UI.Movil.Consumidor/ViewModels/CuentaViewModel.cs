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
        private ObservableCollection<Cuenta> _cuenta;

        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;

        public ObservableCollection<Cuenta> Cuentas
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public Command AgregarCuentaCommand { get; }

        public CuentaViewModel(FactoryManager factoryManager)
        {
            Title = "Cuenta";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            AgregarCuentaCommand = new Command(OnAgregarCuenta);
            ActualizarDatos();

            MessagingCenter.Subscribe<CuentaDetailViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaChanged);
        }

        private void OnCuentaChanged(CuentaDetailViewModel sender, Cuenta pie)
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
        }
    }
}
