using Microsoft.Extensions.Logging;

namespace Leinhart.Web.Application.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class
        CommandHandler<TCommand, TResponse> :
        ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _innerHandler;
        private readonly ILogger<CommandHandler<TCommand, TResponse>> _logger;

        public CommandHandler(ICommandHandler<TCommand, TResponse> innerHandler, ILogger<CommandHandler<TCommand, TResponse>> logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct)
        {
            string commandName = GetDisplayName(typeof(TCommand));

            _logger.LogInformation("Procesando command {Command}", commandName);

            var result = await _innerHandler.Handle(command, ct);

            if (result.HasError)
            {
                _logger.Log(GetLogLevel(result.Type), "Command {Command} terminado con error {Type}: {Message}", commandName, result.Type, result.Message);
            }
            else
            {
                _logger.LogInformation("Command {Command} terminado", commandName);
            }

            return result;
        }
    }

    internal sealed class
        CommandBaseHandler<TCommand> :
        ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _innerHandler;
        private readonly ILogger<CommandBaseHandler<TCommand>> _logger;

        public CommandBaseHandler(ICommandHandler<TCommand> innerHandler, ILogger<CommandBaseHandler<TCommand>> logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public async Task<Result> Handle(TCommand command, CancellationToken ct)
        {
            string commandName = GetDisplayName(typeof(TCommand));

            _logger.LogInformation("Procesando command {Command}", commandName);

            var result = await _innerHandler.Handle(command, ct);

            if (result.HasError)
            {
                _logger.Log(GetLogLevel(result.Type), "Command {Command} terminado con error {Type}: {Message}", commandName, result.Type, result.Message);
            }
            else
            {
                _logger.LogInformation("Command {Command} terminado", commandName);
            }

            return result;
        }
    }

    internal sealed class
        QueryHandler<TQuery, TResponse> :
        IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery, TResponse> _innerHandler;
        private readonly ILogger<QueryHandler<TQuery, TResponse>> _logger;

        public QueryHandler(IQueryHandler<TQuery, TResponse> innerHandler, ILogger<QueryHandler<TQuery, TResponse>> logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct)
        {
            string queryName = GetDisplayName(typeof(TQuery));

            _logger.LogInformation("Procesando query {Query}", queryName);

            var result = await _innerHandler.Handle(query, ct);

            if (result.HasError)
            {
                _logger.Log(GetLogLevel(result.Type), "Query {Query} terminada con error {Type}: {Message}", queryName, result.Type, result.Message);
            }
            else
            {
                _logger.LogInformation("Query {Query} terminada", queryName);
            }

            return result;
        }
    }

    private static LogLevel GetLogLevel(ResultType type)
    {
        return type == ResultType.UnexpectedError ? LogLevel.Error : LogLevel.Warning;
    }

    private static string GetDisplayName(Type type)
    {
        return type.DeclaringType is null ? type.Name : $"{type.DeclaringType.Name}.{type.Name}";
    }
}
