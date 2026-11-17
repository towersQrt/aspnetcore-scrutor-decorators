using Leinhart.Web.Application.Companies.Commands.Create;
using Leinhart.Web.Application.Companies.Commands.Delete;
using Leinhart.Web.Application.Companies.Commands.Update;
using Leinhart.Web.Application.Companies.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Leinhart.Web.Controllers;

[ApiController]
[Route("api/companies")]
public sealed class CompaniesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCompany.Command command,
        [FromServices] ICommandHandler<CreateCompany.Command, int> handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(command, ct));
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        UpdateCompany.Command command,
        [FromServices] ICommandHandler<UpdateCompany.Command, CompanyDto> handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(command, ct));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] ICommandHandler<DeleteCompany.Command> handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(new DeleteCompany.Command(id), ct));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        [FromServices] IQueryHandler<GetCompanyById.Query, CompanyDto> handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(new GetCompanyById.Query(id), ct));
    }
}
