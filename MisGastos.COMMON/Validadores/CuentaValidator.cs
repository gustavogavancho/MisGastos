using FluentValidation;
using MisGastos.COMMON.Entidades;

namespace MisGastos.COMMON.Validadores
{
    public class CuentaValidator : AbstractValidator<Cuenta>
    {
        public CuentaValidator()
        {
            RuleFor(cuenta => cuenta.Nombre).NotEmpty();
            RuleFor(cuenta => cuenta.ImageUrl).NotEmpty();
        }
    }
}
