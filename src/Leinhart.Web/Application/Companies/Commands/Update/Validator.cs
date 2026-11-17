
namespace Leinhart.Web.Application.Companies.Commands.Update;

public static partial class UpdateCompany
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id debe ser mayor a 0");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(200).WithMessage("El nombre no puede tener más de 200 caracteres");
            RuleFor(x => x.Email)
                .MaximumLength(256).WithMessage("El correo no puede tener más de 256 caracteres")
                .EmailAddress().WithMessage("El correo no tiene un formato válido")
                .When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone).MaximumLength(20).WithMessage("El teléfono no puede tener más de 20 caracteres");
        }
    }
}
