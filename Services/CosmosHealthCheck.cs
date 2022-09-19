using CosmosCRUD.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CosmosCRUD.Services
{
    public class CosmosHealthCheck : IHealthCheck
    {
        private readonly CosmosDbService cosmosDbService;

        public CosmosHealthCheck(CosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await cosmosDbService.IsHealthy()
                ? await Task.FromResult(HealthCheckResult.Healthy())
                : await Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
