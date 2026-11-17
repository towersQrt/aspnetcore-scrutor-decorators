namespace Leinhart.Web.Application.Companies.Commands.Update;

public static partial class UpdateCompany
{
    public sealed class Handler : ICommandHandler<Command, CompanyDto>
    {
        public Task<Result<CompanyDto>> Handle(Command command, CancellationToken ct)
        {
            CompanyDto company = new CompanyDto(command.Id, command.Name, command.Email, command.Phone);

            return Task.FromResult(Result<CompanyDto>.Success(company, ResultType.Updated));
        }
    }
}
