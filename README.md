# VM-Azure-Functions
Simple Virtual Machine management with Azure Functions

## Environment variable configuration `local.settings.json`

```js
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AZURE_SUBSCRIPTION_ID": "{SubscriptionId}",
    "AZURE_SERVICE_PRINCIPAL_CLIENT_ID" : "{ClientId}",
    "AZURE_SERVICE_PRINCIPAL_SECRET": "{Secret}",
    "AZURE_SERVICE_PRINCIPAL_TENANT_ID": "{TenantId}",
    "AZURE_STORAGE_CONNECTION_STRING": "{ConnectionString}",
    "AZURE_RESOURCE_GROUP_NAME": "{RGName}",
    "AZURE_REGION": "{Region}",
    "AZURE_STORAGE_CONTAINER_NAME": "{ContainerName}",
    "ARM_TEMPLATE_NAME": "{TemplateName}",
    "ARM_PARAMETERS_NAME": "{ParametersName}",
    "AZURE_DEPLOYMENT_NAME": "{DeploymentName}"
  }
}
```
