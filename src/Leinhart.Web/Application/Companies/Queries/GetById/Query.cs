
namespace Leinhart.Web.Application.Companies.Queries.GetById;

public static partial class GetCompanyById
{
    public sealed record Query(int Id) : IQuery<CompanyDto>;
}
