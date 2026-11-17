
namespace Leinhart.Web.Application.Companies.Commands.Update;

public static partial class UpdateCompany
{
    public sealed record Command(int Id, string Name, string? Email, string? Phone) : ICommand<CompanyDto>;
}
