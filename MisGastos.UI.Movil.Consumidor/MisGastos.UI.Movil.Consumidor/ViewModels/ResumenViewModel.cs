using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Extensions;
using MisGastos.UI.Movil.Consumidor.Models;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class ResumenViewModel : BaseViewModel
    {
        ICuentaManager _cuentaManager;
        IMovimientoManager _movimientoManager;
        ICategoriaManager _categoriaManager;

        private ObservableCollection<Cuenta> _cuentas;
        private Cuenta _cuenta;
        private ObservableCollection<MovimientoModel> _movimientos;
        private decimal _totalIngresos;
        private decimal _totalEgresos;

        public ObservableCollection<Cuenta> Cuentas
        {
            get => _cuentas;
            set => SetProperty(ref _cuentas, value);
        }

        public Cuenta Cuenta
        {
            get
            {
                return _cuenta;
            }
            set
            {
                _cuenta = value;
                LoadMovimientos(value);
            }
        }

        public ObservableCollection<MovimientoModel> Movimientos
        {
            get => _movimientos;
            set => SetProperty(ref _movimientos, value);
        }

        public decimal TotalIngresos
        {
            get => _totalIngresos;
            set => SetProperty(ref _totalIngresos, value);
        }

        public decimal TotalEgresos
        {
            get => _totalEgresos;
            set => SetProperty(ref _totalEgresos, value);
        }

        public ResumenViewModel(FactoryManager factoryManager)
        {
            Title = "Resumen movimientos";
            _cuentaManager = factoryManager.CuentaManager();
            _categoriaManager = factoryManager.CategoriaManager();
            _movimientoManager = factoryManager.MovimientoManager();
            ActualizarDatos();

            //Ingreso - Egreso events
            MessagingCenter.Subscribe<RegistrarEgresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoEgresoChanged);
            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoIngresoChanged);


            //Cuenta
            MessagingCenter.Subscribe<CuentaDetailViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaChanged);
        }

        private void OnCuentaChanged(CuentaDetailViewModel sender, Cuenta cuenta)
        {
            ActualizarDatos();
        }

        private void OnMovimientoIngresoChanged(RegistrarIngresoViewModel sender, Movimiento movimiento)
        {
            ActualizarDatos();
        }

        private void OnMovimientoEgresoChanged(RegistrarEgresoViewModel sender, Movimiento movimiento)
        {
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            Cuentas = _cuentaManager.ObtenerTodo.ToObservableCollection();
            Cuenta = new Cuenta();
            TotalEgresos = default;
            TotalIngresos = default;
        }

        private void LoadMovimientos(Cuenta value)
        {
            try
            {
                Movimientos = new ObservableCollection<MovimientoModel>();
                foreach (var item in _movimientoManager.BuscarPorCuenta(value.Id).ToObservableCollection())
                {
                    Cuenta cuenta = _cuentaManager.SearchById(item.IdCuenta);
                    Categoria categoria = _categoriaManager.SearchById(item.IdCategoria);

                    Movimientos.Add(new MovimientoModel
                    {
                        Movimiento = item,
                        NombreCuenta = cuenta.Nombre,
                        TipoCategoria = categoria.TipoCategoria.ToString(),
                        ImageUrl = (categoria.TipoCategoria == TipoCategoria.Ingresos) ? "icon_ingreso.png" : "icon_egreso.png",
                        Background = (categoria.TipoCategoria == TipoCategoria.Ingresos) ? "LightBlue" : "White",
                        DescripcionMovimiento = item.Descripcion,
                    });

                    TotalIngresos = Movimientos.Where(x => x.TipoCategoria == TipoCategoria.Ingresos.ToString()).Sum(x => x.Movimiento.Monto);
                    TotalEgresos = Movimientos.Where(x => x.TipoCategoria == TipoCategoria.Gastos.ToString()).Sum(x => x.Movimiento.Monto);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
