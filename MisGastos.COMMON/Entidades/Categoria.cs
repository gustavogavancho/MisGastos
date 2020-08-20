using MisGastos.COMMON.Enumeraciones;

namespace MisGastos.COMMON.Entidades
{
    public class Categoria : BaseDTO
    {
        public TipoCategoria TipoCategoria { get; set; }
        public string Nombre { get; set; }
        public string ImageUrl { get; set; }
    }
}
