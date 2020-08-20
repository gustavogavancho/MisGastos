using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using System.Collections.Generic;

namespace MisGastos.COMMON.Interfaces
{
    public interface ICategoriaManager : IGenericManager<Categoria>
    {
        Categoria BuscarPorNombre(string nombre);

        IEnumerable<Categoria> BuscarPorTipoCategoria(TipoCategoria tipoCategoria);
    }
}
