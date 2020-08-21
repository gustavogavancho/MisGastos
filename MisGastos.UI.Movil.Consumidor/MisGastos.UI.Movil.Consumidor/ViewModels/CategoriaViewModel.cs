using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Entensions;
using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private ObservableCollection<Categoria> _categoria;

        FactoryManager _factoryManager;
        ICategoriaManager _categoriaManager;

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
            _factoryManager = factoryManager;
            _categoriaManager = factoryManager.CategoriaManager();
            AgregarCategoriaCommand = new Command(OnAgregarCategoria);
            CategoriaSelectedCommnad = new Command<Categoria>(OnCategoriaSelected);
            ActualizarDatos();

            MessagingCenter.Subscribe<CategoriaDetailViewModel, Categoria>
                (this, MessageNames.CategoriaChangedMessage, OnCategoriaChanged);

        }

        private async void OnCategoriaSelected(Categoria categoria)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoriaDetailPage)}?{nameof(CategoriaDetailViewModel.CategoriaId)}={categoria.Id}");
        }

        private void OnCategoriaChanged(CategoriaDetailViewModel sender, Categoria categoria)
        {
            Categorias = new ObservableCollection<Categoria>(_categoriaManager.ObtenerTodo.OrderBy(x => x.TipoCategoria));
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
