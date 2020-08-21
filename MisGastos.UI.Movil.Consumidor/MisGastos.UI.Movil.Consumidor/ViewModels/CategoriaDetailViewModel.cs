﻿using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    [QueryProperty(nameof(CategoriaId), nameof(CategoriaId))]
    public class CategoriaDetailViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICategoriaManager _categoriaManager;

        private Categoria _categoria;
        private string _categoriaId;
        private IEnumerable<TipoCategoria> _tipoCategoria;

        public IEnumerable<TipoCategoria> TipoCategoria
        {
            get => Enum.GetValues(typeof(TipoCategoria)).Cast<TipoCategoria>();
            set => SetProperty(ref _tipoCategoria, value);
        }


        public Categoria Categoria
        {
            get => _categoria;
            set => SetProperty(ref _categoria, value);
        }

        public string CategoriaId
        {
            get
            {
                return _categoriaId;
            }
            set
            {
                _categoriaId = value;
                LoadCategoriaId(value);
            }
        }

        public Command GuardarCategoriaCommnad { get;  }
        public Command EliminarCategoriaCommand { get;  }
        public Command RegresarCommand { get; set; }
        public Command OnApperaringCommand { get; }
        public Command OnDisappearingCommand { get; }

        public CategoriaDetailViewModel(FactoryManager factoryManager)
        {
            Title = "Detalle categoria";
            _factoryManager = factoryManager;
            _categoriaManager = _factoryManager.CategoriaManager();
            GuardarCategoriaCommnad = new Command(OnGuardarCateoria);
            EliminarCategoriaCommand = new Command(OnEliminarCategoria);
            RegresarCommand = new Command(OnRegresar);
            OnApperaringCommand = new Command(OnApperaring);
            OnDisappearingCommand = new Command(OnDisappearing);
            ActualizarDatos();
        }

        private void OnApperaring(object obj)
        {
            Shell.Current.Navigating += Current_Navigating;
        }

        private void OnDisappearing(object obj)
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

        private void LoadCategoriaId(string categoriaId)
        {
            try
            {
                Categoria = _categoriaManager.SearchById(categoriaId);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private void ActualizarDatos()
        {
            Categoria = new Categoria();
        }

        private async void OnEliminarCategoria(object obj)
        {
            _categoriaManager.Eliminar(CategoriaId);

            MessagingCenter.Send(this, MessageNames.CategoriaChangedMessage, Categoria);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }

        private async void OnGuardarCateoria(object obj)
        {
            if (string.IsNullOrEmpty(Categoria.Id))
            {
                _categoriaManager.Insertar(Categoria);
            }
            else
            {
                _categoriaManager.Actualizar(Categoria);
            }

            MessagingCenter.Send(this, MessageNames.CategoriaChangedMessage, Categoria);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }
    }
}
