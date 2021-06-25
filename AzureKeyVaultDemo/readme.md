# Azure Key Vault Demo - Documentation
The purpose of this solution is to demonstrate acquiring a connection string stored in [Azure Key Vault](https://docs.microsoft.com/en-us/azure/key-vault/), utilizing [Azure Config Builder](https://docs.microsoft.com/en-us/aspnet/config-builder#azurekeyvaultconfigbuilder). 

The intention of this solution is to be hosted in [Azure Web App](https://azure.microsoft.com/en-us/services/app-service/)

# Dependencies

In order to use Config Builder, these two nuget dependencies are necessary:

1. Microsoft.Configuration.ConfigurationBuilders.Azure
2. Microsoft.Configuration.ConfigurationBuilders.Base

The solution also assumes that the code will be hosted in, and the data will be hosted in Azure SQL. 

A connection to SQL is not actually necessary to demonstrate how the app consumes Secret values from the key vault.

If you want to just see the demonstration of connectionStrings dynamically generating from key vault, without *actually* making the connection to SQL, then refer to the appSetting GetValuesFromDatabase in web.config, change it to "false".

