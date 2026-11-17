namespace Leinhart.Web.Application.Companies.Commands.Delete;

public static partial class DeleteCompany
{
    public sealed class Handler : ICommandHandler<Command>
    {
        public Task<Result> Handle(Command command, CancellationToken ct)
        {
            return Task.FromResult(Result.Success(ResultType.Deleted));
        }
    }
}
