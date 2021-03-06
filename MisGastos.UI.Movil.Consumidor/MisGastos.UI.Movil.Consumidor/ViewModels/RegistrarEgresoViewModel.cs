﻿using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Extensions;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    [QueryProperty(nameof(MovimientoId), nameof(MovimientoId))]
    public class RegistrarEgresoViewModel : BaseViewModel
    {
        ICuentaManager _cuentaManager;
        ICategoriaManager _categoriaManager;
        IMovimientoManager _movimientoManager;
        IBalanceManager _balanceManager;

        private ObservableCollection<Cuenta> _cuentas;
        private ObservableCollection<Categoria> _categorias;
        private Movimiento _movimiento;
        private Cuenta _cuenta;
        private Categoria _categoria;
        private Balance _balance;
        private string _movimientoId;
        private Cuenta _cuentaClean;

        public ObservableCollection<Cuenta> Cuentas
        {
            get => _cuentas;
            set => SetProperty(ref _cuentas, value);
        }

        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set => SetProperty(ref _categorias, value);
        }

        public Movimiento Movimiento
        {
            get => _movimiento;
            set => SetProperty(ref _movimiento, value);
        }

        public Cuenta Cuenta
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public Categoria Categoria
        {
            get => _categoria;
            set => SetProperty(ref _categoria, value);
        }

        public Balance Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        public string MovimientoId
        {
            get
            {
                return _movimientoId;
            }
            set
            {
                _movimientoId = value;
                LoadMovimientoId(value);
            }
        }

        public Command GuardarMovimientoCommnad { get; }
        public Command EliminarMovimientoCommand { get; }
        public Command RegresarCommand { get; }
        public Command OnApperaringCommand { get; }
        public Command OnDisappearingCommand { get; }

        public RegistrarEgresoViewModel(FactoryManager factoryManager)
        {
            Title = "Registrar Egreso";
            _cuentaManager = factoryManager.CuentaManager();
            _categoriaManager = factoryManager.CategoriaManager();
            _movimientoManager = factoryManager.MovimientoManager();
            _balanceManager = factoryManager.BalanceManager();
            GuardarMovimientoCommnad = new Command(OnGuardarMovimineto);
            EliminarMovimientoCommand = new Command(OnEliminarMovmiento);
            RegresarCommand = new Command(OnRegresar);
            OnApperaringCommand = new Command(OnApperaring);
            OnDisappearingCommand = new Command(OnDisappearing);
            ActualizarDatos();

            MessagingCenter.Subscribe<CuentaDetailViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaChanged);

            MessagingCenter.Subscribe<CategoriaDetailViewModel, Categoria>
                (this, MessageNames.CategoriaChangedMessage, OnCategoriaChanged);

        }

        private void OnCategoriaChanged(CategoriaDetailViewModel sender, Categoria categoria)
        {
            ActualizarDatos();
        }

        private void OnCuentaChanged(CuentaDetailViewModel sender, Cuenta cuenta)
        {
            ActualizarDatos();
        }

        private async void OnEliminarMovmiento(object obj)
        {
            if (await Shell.Current.DisplayAlert("Aviso", $"¿Estas seguro de querer eliminar el movimiento \"{Movimiento.Descripcion}\"?", "Si", "No"))
            {
                Cuenta cuentaToEdit = _cuentaManager.SearchById(Movimiento.IdCuenta);
                cuentaToEdit.Balance += Movimiento.Monto;
                Balance balanceToEdit = _balanceManager.ObtenerTodo.FirstOrDefault();
                balanceToEdit.Egresos += Movimiento.Monto;
                balanceToEdit.BalanceGeneral += Movimiento.Monto;

                _balanceManager.Actualizar(balanceToEdit);
                _cuentaManager.Actualizar(cuentaToEdit);
                _movimientoManager.Eliminar(MovimientoId);

                //Send messages
                MessagingCenter.Send(this, MessageNames.MovimientoChangedMessage, Movimiento);
                MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);
                MessagingCenter.Send(this, MessageNames.BalanceChangedMessage, Balance);

                await Shell.Current.Navigation.PopAsync();

                ActualizarDatos(); 
            }
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

        private async void Current_Navigating(object sender, ShellNavigatingEventArgs e)
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
            await Shell.Current.Navigation.PopAsync();
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            Cuenta = new Cuenta();
            Categoria = new Categoria();
            Movimiento = new Movimiento();
            Cuentas = _cuentaManager.ObtenerTodo.ToObservableCollection();
            Categorias = _categoriaManager.ObtenerTodo.Where(x => x.TipoCategoria == TipoCategoria.Gastos).OrderBy(x => x.TipoCategoria).ToObservableCollection();
        }

        private void LoadMovimientoId(string movimientoId)
        {
            try
            {
                Movimiento = _movimientoManager.SearchById(movimientoId);
                Categoria = _categoriaManager.SearchById(Movimiento.IdCategoria);
                Cuenta = _cuentaManager.SearchById(Movimiento.IdCuenta);
                _cuentaClean = new Cuenta();
                _cuentaClean = _cuentaManager.SearchById(Movimiento.IdCuenta);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnGuardarMovimineto(object obj)
        {
            Movimiento.IdCategoria = (Categoria != null) ? Categoria.Id : default;
            Movimiento.IdCuenta = (Cuenta != null) ? Cuenta.Id : default;
            Movimiento.Fecha = DateTime.Now;
            Cuenta cuentaToEdit = (Cuenta != null) ? _cuentaManager.SearchById(Cuenta.Id) : new Cuenta();
            Balance balanceToEdit = _balanceManager.ObtenerTodo.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(Movimiento.Id))
            {
                cuentaToEdit.Balance -= Movimiento.Monto;
                balanceToEdit.Egresos -= Movimiento.Monto;
                balanceToEdit.BalanceGeneral -= Movimiento.Monto;

                if (_movimientoManager.Insertar(Movimiento) is null)
                {
                    await Shell.Current.DisplayAlert("Advertencia", _movimientoManager.Error, "Aceptar");
                    return;
                }
            }
            else
            {
                Movimiento movimientoToEdit = _movimientoManager.SearchById(Movimiento.Id);
                decimal corregirMonto = 0;
                decimal corregirEgreso = 0;
                decimal corregirBalanceGeneral = 0;
                decimal corregirMontoCuenta = 0;

                if (movimientoToEdit.Monto > Movimiento.Monto)
                {
                    corregirMonto = movimientoToEdit.Monto - Movimiento.Monto;
                    corregirEgreso = balanceToEdit.Egresos + corregirMonto;
                    corregirBalanceGeneral = balanceToEdit.BalanceGeneral + corregirMonto;
                    corregirMontoCuenta = cuentaToEdit.Balance + corregirEgreso;

                    //TODO:
                    balanceToEdit.Egresos = corregirEgreso;
                    balanceToEdit.BalanceGeneral = corregirBalanceGeneral;
                    cuentaToEdit.Balance = corregirMontoCuenta;

                    if (_movimientoManager.Actualizar(Movimiento) is null)
                    {
                        await Shell.Current.DisplayAlert("Advertencia", _movimientoManager.Error, "Aceptar");
                        return;
                    }

                    if (Movimiento.IdCuenta != movimientoToEdit.IdCuenta)
                    {
                        Cuenta secondCuentaToEdit = new Cuenta();
                        secondCuentaToEdit = _cuentaManager.SearchById(_cuentaClean.Id);
                        secondCuentaToEdit.Balance += Movimiento.Monto + corregirMonto;
                        //cuentaToEdit.Balance -= movimientoToEdit.Monto;
                        //cuentaToEdit.Balance -= Movimiento.Monto;
                        _cuentaManager.Actualizar(cuentaToEdit);
                        _cuentaManager.Actualizar(secondCuentaToEdit);
                        _cuentaClean = new Cuenta();
                    }
                }
                else if (movimientoToEdit.Monto < Movimiento.Monto)
                {
                    corregirMonto = Movimiento.Monto - movimientoToEdit.Monto;
                    corregirEgreso = balanceToEdit.Egresos - corregirMonto;
                    corregirBalanceGeneral = balanceToEdit.BalanceGeneral - corregirMonto;
                    corregirMontoCuenta = cuentaToEdit.Balance - corregirMonto;

                    //TODO:
                    balanceToEdit.Egresos = corregirEgreso;
                    balanceToEdit.BalanceGeneral = corregirBalanceGeneral;
                    cuentaToEdit.Balance = corregirMontoCuenta;

                    if (_movimientoManager.Actualizar(Movimiento) is null)
                    {
                        await Shell.Current.DisplayAlert("Advertencia", _movimientoManager.Error, "Aceptar");
                        return;
                    }

                    if (Movimiento.IdCuenta != movimientoToEdit.IdCuenta)
                    {
                        Cuenta secondCuentaToEdit = new Cuenta();
                        secondCuentaToEdit = _cuentaManager.SearchById(_cuentaClean.Id);
                        secondCuentaToEdit.Balance += Movimiento.Monto - corregirMonto;
                        cuentaToEdit.Balance -= movimientoToEdit.Monto;
                        _cuentaManager.Actualizar(cuentaToEdit);
                        _cuentaManager.Actualizar(secondCuentaToEdit);
                        _cuentaClean = new Cuenta();
                    }
                }
                else if (movimientoToEdit.Monto == Movimiento.Monto)
                {
                    if (_movimientoManager.Actualizar(Movimiento) is null)
                    {
                        await Shell.Current.DisplayAlert("Advertencia", _movimientoManager.Error, "Aceptar");
                        return;
                    }
                    else
                    {
                        if (Movimiento.IdCuenta != movimientoToEdit.IdCuenta)
                        {
                            Cuenta secondCuentaToEdit = new Cuenta();
                            secondCuentaToEdit = _cuentaManager.SearchById(_cuentaClean.Id);
                            secondCuentaToEdit.Balance += Movimiento.Monto;
                            cuentaToEdit.Balance -= Movimiento.Monto;
                            _cuentaManager.Actualizar(cuentaToEdit);
                            _cuentaManager.Actualizar(secondCuentaToEdit);
                            _cuentaClean = new Cuenta();
                        }

                        //MessagingCenter.Send(this, MessageNames.MovimientoChangedMessage, Movimiento);
                        //MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);
                    }
                    await Shell.Current.Navigation.PopAsync();
                    return;
                }
            }

            _cuentaManager.Actualizar(cuentaToEdit);
            _balanceManager.Actualizar(balanceToEdit);

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);
            MessagingCenter.Send(this, MessageNames.MovimientoChangedMessage, Movimiento);
            MessagingCenter.Send(this, MessageNames.BalanceChangedMessage, Balance);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }
    }
}
