using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using System.IO;

namespace Sample.Functions
{
    public static class CreateMachine
    {
        [FunctionName("CreateMachine")]
        public static async Task Run([TimerTrigger("0 0 16 * * 5-0")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            var subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
            var clientId = Environment.GetEnvironmentVariable("AZURE_SERVICE_PRINCIPAL_CLIENT_ID");
            var secret = Environment.GetEnvironmentVariable("AZURE_SERVICE_PRINCIPAL_SECRET");
            var tenantId = Environment.GetEnvironmentVariable("AZURE_SERVICE_PRINCIPAL_TENANT_ID");
            var resourceGroupName = Environment.GetEnvironmentVariable("AZURE_RESOURCE_GROUP_NAME");
            var region = Environment.GetEnvironmentVariable("AZURE_REGION");
            var storageContainerName = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER_NAME");
            var armTemplateName = Environment.GetEnvironmentVariable("ARM_TEMPLATE_NAME");
            var armParametersName = Environment.GetEnvironmentVariable("ARM_PARAMETERS_NAME");
            var deploymentName = Environment.GetEnvironmentVariable("AZURE_DEPLOYMENT_NAME");

            var credentials = SdkContext
                .AzureCredentialsFactory
                .FromServicePrincipal(clientId, secret, tenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Microsoft.Azure.Management.Fluent.Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            var rgExists = await azure.ResourceGroups.ContainAsync(resourceGroupName);
            if(!rgExists) 
            {
                await azure.ResourceGroups
                    .Define(resourceGroupName)
                    .WithRegion(region)
                    .CreateAsync();
            }

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(storageContainerName);
            var templateClient = blobContainerClient.GetBlobClient(armTemplateName);
            var parametersClient = blobContainerClient.GetBlobClient(armParametersName);
            var template = await templateClient.DownloadAsync();
            var parameters = await parametersClient.DownloadAsync();

            var templateReader = new StreamReader(template.Value.Content);
            var parametersReader = new StreamReader(parameters.Value.Content);
            var templateContent = await templateReader.ReadToEndAsync();
            var parametersContent = await parametersReader.ReadToEndAsync();
            
            azure.Deployments
                .Define(deploymentName)
                .WithExistingResourceGroup(resourceGroupName)
                .WithTemplate(templateContent)
                .WithParameters(parametersContent)
                .WithMode(DeploymentMode.Incremental)
                .CreateAsync();
        }
    }
}
