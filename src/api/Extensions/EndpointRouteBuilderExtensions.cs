
using System.Threading;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/insurance/{insuranceId}",
                    (
                        [FromServices] IRepository<Insurance> insuranceRepository,
                        [FromRoute] int insuranceId,
                        CancellationToken cancellationToken) =>
                            insuranceRepository.GetById(insuranceId)
                    )
                    .WithName("GetInsurance")
                    .WithMetadata(new SwaggerOperationAttribute("Get insurance", "Get a insurance by id"));
        endpointRouteBuilder.MapGet("/insurance",
                    (
                        [FromServices] IRepository<Insurance> insuranceRepository,
                        CancellationToken cancellationToken) =>
                            insuranceRepository.GetAll()
                    )
                    .WithName("GetInsurances")
                    .WithMetadata(new SwaggerOperationAttribute("Get insurances", "Get all insurances"));
        endpointRouteBuilder.MapPost("/insurance",
                    (
                        [FromServices] IRepository<Insurance> insuranceRepository,
                        [FromBody] Insurance insurance,
                        CancellationToken cancellationToken) =>
                    {
                        insuranceRepository.Insert(insurance);
                        insuranceRepository.Save();
                    }
                    )
                    .WithName("InsertInsurance")
                    .WithMetadata(new SwaggerOperationAttribute("Insert insurance", "Insert an insurance"));
        endpointRouteBuilder.MapDelete("/insurance/{insuranceId}",
                    (
                        [FromServices] IRepository<Insurance> insuranceRepository,
                        [FromRoute] int insuranceId,
                        CancellationToken cancellationToken) =>
                    {
                        insuranceRepository.Delete(insuranceId);
                        insuranceRepository.Save();
                    }
                    )
                    .WithName("DeleteInsurance")
                    .WithMetadata(new SwaggerOperationAttribute("Delete insurance", "Delete an insurance"));
        endpointRouteBuilder.MapPut("/insurance",
                    (
                        [FromServices] IRepository<Insurance> insuranceRepository,
                        [FromBody] Insurance insurance,
                        CancellationToken cancellationToken) =>
                    {
                        insuranceRepository.Update(insurance);
                        insuranceRepository.Save();
                    }
                    )
                    .WithName("UpdateInsurance")
                    .WithMetadata(new SwaggerOperationAttribute("Update insurance", "Update an insurance"));
        return endpointRouteBuilder;
    }
}