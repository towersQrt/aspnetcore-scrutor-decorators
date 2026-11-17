
namespace Leinhart.Web.Application.Companies.Queries.GetById;

public static partial class GetCompanyById
{
    internal sealed class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id debe ser mayor a 0");
        }
    }
}
