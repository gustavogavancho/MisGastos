using System.ComponentModel;

namespace MisGastos.COMMON.Entidades
{
    public class Cuenta : BaseDTO, INotifyPropertyChanged
    {
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

        private decimal _balance;

        public decimal Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
