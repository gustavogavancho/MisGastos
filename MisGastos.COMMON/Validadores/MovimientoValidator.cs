using FluentValidation;
using MisGastos.COMMON.Entidades;

namespace MisGastos.COMMON.Validadores
{
    public class MovimientoValidator : AbstractValidator<Movimiento>
    {
        public MovimientoValidator()
        {
            RuleFor(movimiento => movimiento.IdCuenta).NotEmpty();
            RuleFor(movimiento => movimiento.IdCategoria).NotEmpty();
            RuleFor(movimiento => movimiento.Monto).NotEmpty();
            RuleFor(movimiento => movimiento.Descripcion).NotEmpty();
        }
    }
}
