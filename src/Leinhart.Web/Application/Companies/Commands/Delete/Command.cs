
namespace Leinhart.Web.Application.Companies.Commands.Delete;

public static partial class DeleteCompany
{
    public sealed record Command(int Id) : ICommand;
}
