namespace Leinhart.Web.Application.Companies.Commands.Delete;

public static partial class DeleteCompany
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id debe ser mayor a 0");
        }
    }
}
