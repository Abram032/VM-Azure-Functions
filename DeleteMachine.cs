using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Threading.Tasks;

namespace Sample.Functions
{
    public static class DeleteMachine
    {
        [FunctionName("DeleteMachine")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
            var clientId = Environment.GetEnvironmentVariable("AZURE_SERVICE_PRINCIPAL_CLIENT_ID");
            var secret = Environment.GetEnvironmentVariable("AZURE_SERVICE_PRINCIPAL_SECRET");
            var tenantId = Environment.GetEnvironmentVariable("AZURE_SERVICE_PRINCIPAL_TENANT_ID");
            var resourceGroupName = Environment.GetEnvironmentVariable("AZURE_RESOURCE_GROUP_NAME");

            var credentials = SdkContext
                .AzureCredentialsFactory
                .FromServicePrincipal(clientId, secret, tenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Microsoft.Azure.Management.Fluent.Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            var rgExists = await azure.ResourceGroups.ContainAsync(resourceGroupName);
            if(rgExists) 
            {
                await azure.ResourceGroups.DeleteByNameAsync(resourceGroupName);
            }
        }
    }
}
