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
    "AZURE_STORAGE_CONNECTION_STRING": "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;",
    "AZURE_RESOURCE_GROUP_NAME": "vm-test-rg",
    "AZURE_REGION": "westeurope",
    "AZURE_STORAGE_CONTAINER_NAME": "templates",
    "ARM_TEMPLATE_NAME": "template.json",
    "ARM_PARAMETERS_NAME": "parameters.json",
    "AZURE_DEPLOYMENT_NAME": "VMTestDeployment"
  }
}
```