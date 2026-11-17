# aspnetcore-scrutor-decorators

Ejemplo mínimo de decoradores con Scrutor en .NET 10. Cada command y query pasa por un decorador de validación (FluentValidation) y uno de logging, sin que el handler lo sepa.

## Cómo funciona

`Scan` registra los handlers del assembly y `Decorate` los envuelve:

```csharp
services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
```

El último `Decorate` queda como capa externa: la petición pasa por logging, luego validación y al final el handler.

`CompaniesController` llama a los handlers de `Create`, `Update`, `Delete` y `GetById` de `src/Leinhart.Web/Application/Companies`.

## Ejecutar

```bash
dotnet run --project src/Leinhart.Web
curl -H "Content-Type: application/json" -d '{"name":"Acme"}' http://localhost:5282/api/companies
curl -H "Content-Type: application/json" -d '{"name":""}' http://localhost:5282/api/companies
```

La segunda petición falla en validación. Ambas quedan registradas en la consola.
