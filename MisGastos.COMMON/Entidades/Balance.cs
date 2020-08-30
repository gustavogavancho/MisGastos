using System.ComponentModel;

namespace MisGastos.COMMON.Entidades
{
    public class Balance : BaseDTO, INotifyPropertyChanged
    {
        private decimal _ingresos;

        public decimal Ingresos
        {
            get => _ingresos;
            set 
            {
                _ingresos = value;
                RaisePropertyChanged(nameof(Ingresos));
            }
        }

        private decimal _egresos;

        public decimal Egresos
        {
            get => _egresos;
            set 
            {
                _egresos = value;
                RaisePropertyChanged(nameof(Egresos));
            }
        }

        private decimal _balanceGeneral;

        public decimal BalanceGeneral
        {
            get => _balanceGeneral;
            set 
            {
                _balanceGeneral = value;
                RaisePropertyChanged(nameof(BalanceGeneral));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
