using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MisGastos.BIZ
{
    public class CategoriaManager : GenericManager<Categoria>, ICategoriaManager
    {
        public CategoriaManager(IGenericRepository<Categoria> repositorio) : base(repositorio)
        {

        }

        public Categoria BuscarPorNombre(string nombre)
        {
            return _repositorio.Query(categoria => categoria.Nombre == nombre).FirstOrDefault();
        }

        public IEnumerable<Categoria> BuscarPorTipoCategoria(TipoCategoria tipoCategoria)
        {
            return _repositorio.Query(categoria => categoria.TipoCategoria == tipoCategoria);
        }
    }
}
