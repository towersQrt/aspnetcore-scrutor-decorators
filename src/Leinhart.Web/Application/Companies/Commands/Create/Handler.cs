namespace Leinhart.Web.Application.Companies.Commands.Create;

public static partial class CreateCompany
{
    public sealed class Handler : ICommandHandler<Command, int>
    {
        public Task<Result<int>> Handle(Command command, CancellationToken ct)
        {
            return Task.FromResult(Result<int>.Success(1, ResultType.Created));
        }
    }
}
