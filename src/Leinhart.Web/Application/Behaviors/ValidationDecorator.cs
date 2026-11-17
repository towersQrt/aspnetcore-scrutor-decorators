using FluentValidation.Results;

namespace Leinhart.Web.Application.Behaviors;

internal static class ValidationDecorator
{
    internal sealed class
        CommandHandler<TCommand, TResponse> :
        ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _innerHandler;
        private readonly IEnumerable<IValidator<TCommand>> _lstValidator;

        public CommandHandler(ICommandHandler<TCommand, TResponse> innerHandler, IEnumerable<IValidator<TCommand>> lstValidator)
        {
            _innerHandler = innerHandler;
            _lstValidator = lstValidator;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct)
        {
            ValidationFailure[] lstValidationFailure = await ValidateAsync(command, _lstValidator, ct);

            if (lstValidationFailure.Length == 0)
            {
                return await _innerHandler.Handle(command, ct);
            }

            return Result<TResponse>.Failure(GetMessage(lstValidationFailure), ResultType.Validation);
        }
    }

    internal sealed class
        CommandBaseHandler<TCommand> :
        ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _innerHandler;
        private readonly IEnumerable<IValidator<TCommand>> _lstValidator;

        public CommandBaseHandler(ICommandHandler<TCommand> innerHandler, IEnumerable<IValidator<TCommand>> lstValidator)
        {
            _innerHandler = innerHandler;
            _lstValidator = lstValidator;
        }

        public async Task<Result> Handle(TCommand command, CancellationToken ct)
        {
            ValidationFailure[] lstValidationFailure = await ValidateAsync(command, _lstValidator, ct);

            if (lstValidationFailure.Length == 0)
            {
                return await _innerHandler.Handle(command, ct);
            }

            return Result.Failure(GetMessage(lstValidationFailure), ResultType.Validation);
        }
    }

    internal sealed class
        QueryHandler<TQuery, TResponse> :
        IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery, TResponse> _innerHandler;
        private readonly IEnumerable<IValidator<TQuery>> _lstValidator;

        public QueryHandler(IQueryHandler<TQuery, TResponse> innerHandler, IEnumerable<IValidator<TQuery>> lstValidator)
        {
            _innerHandler = innerHandler;
            _lstValidator = lstValidator;
        }

        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct)
        {
            ValidationFailure[] lstValidationFailure = await ValidateAsync(query, _lstValidator, ct);

            if (lstValidationFailure.Length == 0)
            {
                return await _innerHandler.Handle(query, ct);
            }

            return Result<TResponse>.Failure(GetMessage(lstValidationFailure), ResultType.Validation);
        }
    }

    private static async Task<ValidationFailure[]> ValidateAsync<TRequest>(TRequest request, IEnumerable<IValidator<TRequest>> lstValidator, CancellationToken ct)
    {
        if (!lstValidator.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] lstValidationResult = await Task.WhenAll(lstValidator.Select(x => x.ValidateAsync(context, ct)));

        return lstValidationResult
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToArray();
    }

    private static string GetMessage(ValidationFailure[] lstValidationFailure)
    {
        return lstValidationFailure[0].ErrorMessage;
    }
}
