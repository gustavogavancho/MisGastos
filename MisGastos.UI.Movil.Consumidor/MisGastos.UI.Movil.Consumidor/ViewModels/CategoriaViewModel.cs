﻿using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.Views;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        ICategoriaManager _categoriaManager;

        private ObservableCollection<Categoria> _categoria;

        public ObservableCollection<Categoria> Categorias
        {
            get => _categoria;
            set => SetProperty(ref _categoria, value);
        }

        public Command AgregarCategoriaCommand { get; }
        public Command CategoriaSelectedCommnad { get; }

        public CategoriaViewModel(FactoryManager factoryManager)
        {
            Title = "Categoria";
            _categoriaManager = factoryManager.CategoriaManager();
            AgregarCategoriaCommand = new Command(OnAgregarCategoria);
            CategoriaSelectedCommnad = new Command<Categoria>(OnCategoriaSelected);
            ActualizarDatos();

            MessagingCenter.Subscribe<CategoriaDetailViewModel, Categoria>
                (this, MessageNames.CategoriaChangedMessage, OnCategoriaChanged);

            //Config events
            MessagingCenter.Subscribe<ConfiguracionesViewModel, Categoria>
                (this, MessageNames.CategoriaChangedMessage, OnCategoriaConfigChanged);

        }

        private async void OnCategoriaSelected(Categoria categoria)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoriaDetailPage)}?{nameof(CategoriaDetailViewModel.CategoriaId)}={categoria.Id}");
        }

        private void OnCategoriaConfigChanged(ConfiguracionesViewModel arg1, Categoria arg2)
        {
            ActualizarDatos();
        }

        private void OnCategoriaChanged(CategoriaDetailViewModel sender, Categoria categoria)
        {
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            Categorias = new ObservableCollection<Categoria>(_categoriaManager.ObtenerTodo.OrderBy(x => x.TipoCategoria));
        }

        private async void OnAgregarCategoria(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CategoriaDetailPage));
        }
    }
}
