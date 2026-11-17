namespace Leinhart.Web.Application.Companies.Queries.GetById;

public static partial class GetCompanyById
{
    public sealed class Handler : IQueryHandler<Query, CompanyDto>
    {
        public Task<Result<CompanyDto>> Handle(Query query, CancellationToken ct)
        {
            CompanyDto company = new CompanyDto(query.Id, "Acme", null, null);

            return Task.FromResult(Result<CompanyDto>.Success(company));
        }
    }
}
