namespace Leinhart.Web.Application.Companies.Commands.Create;

public static partial class CreateCompany
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(200).WithMessage("El nombre no puede tener más de 200 caracteres");
        }
    }
}
