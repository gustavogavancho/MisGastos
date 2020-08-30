using MisGastos.COMMON.Enumeraciones;
using System.ComponentModel;

namespace MisGastos.COMMON.Entidades
{
    public class Categoria : BaseDTO, INotifyPropertyChanged
    {
        private TipoCategoria _tipoCategoria;

        public TipoCategoria TipoCategoria
        {
            get => _tipoCategoria; 
            set 
            {
                _tipoCategoria = value;
                RaisePropertyChanged(nameof(TipoCategoria));
            }
        }

        private string _nombre;

        public string Nombre
        {
            get => _nombre;
            set 
            {
                _nombre = value;
                RaisePropertyChanged(nameof(Nombre));
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
