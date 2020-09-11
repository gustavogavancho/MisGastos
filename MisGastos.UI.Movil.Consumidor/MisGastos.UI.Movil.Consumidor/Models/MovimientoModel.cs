using MisGastos.COMMON.Entidades;

namespace MisGastos.UI.Movil.Consumidor.Models
{
    public class MovimientoModel
    {
        public Movimiento Movimiento { get; set; }
        public string TipoCategoria { get; set; }
        public string NombreCuenta { get; set; }
        public string DescripcionMovimiento { get; set; }
        public string ImageUrl { get; set; }
        public string Background { get; set; }

        public override string ToString()
        {
            return $"{NombreCuenta} - {Movimiento.Fecha.ToShortDateString()}"; 
        }
    }
}
