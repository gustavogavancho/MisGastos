using System;
using System.ComponentModel;

namespace MisGastos.COMMON.Entidades
{
    public class Movimiento : BaseDTO, INotifyPropertyChanged
    {
        private string _idCuenta;

        public string IdCuenta
        {
            get => _idCuenta;
            set 
            {
                _idCuenta = value;
                RaisePropertyChanged(nameof(IdCuenta));
            }
        }

        private string _idCategoria;

        public string IdCategoria
        {
            get => _idCategoria;
            set 
            { 
                _idCategoria = value;
                RaisePropertyChanged(nameof(IdCategoria));
            }
        }

        private decimal _monto;

        public decimal Monto
        {
            get => _monto;
            set 
            {
                _monto = value;
                RaisePropertyChanged(nameof(Monto));
            }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get => _imageUrl;
            set 
            { 
                _imageUrl = value;
                RaisePropertyChanged(nameof(ImageUrl));
            }
        }

        private string _descripcion;

        public string Descripcion
        {
            get => _descripcion;
            set 
            {
                _descripcion = value;
                RaisePropertyChanged(nameof(Descripcion));
            }
        }

        private DateTime _fecha;

        public DateTime Fecha
        {
            get => _fecha;
            set 
            {
                _fecha = value;
                RaisePropertyChanged(nameof(Fecha));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
