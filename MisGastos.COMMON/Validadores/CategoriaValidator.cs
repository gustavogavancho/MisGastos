using FluentValidation;
using MisGastos.COMMON.Entidades;

namespace MisGastos.COMMON.Validadores
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(categoria => categoria.TipoCategoria).NotEmpty();
            RuleFor(categoria => categoria.Nombre).NotEmpty();
            RuleFor(categoria => categoria.ImageUrl).NotEmpty();
        }
    }
}
