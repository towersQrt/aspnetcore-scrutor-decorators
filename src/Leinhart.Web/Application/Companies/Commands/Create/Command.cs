
namespace Leinhart.Web.Application.Companies.Commands.Create;

public static partial class CreateCompany
{
    public sealed record Command(string Name) : ICommand<int>;
}
