[Azure Key Vault](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-overview)

### Login using Azure CLI
```
az login
```
### Create a resouce group: SD1597
```
az group create --name "SD1597" --location "Southeast Asia"
```
### Create a Key Vault called "SD1597" within group "SD1597"
```
az keyvault create --name "SD1597" --resource-group "SD1597" --location "Southeast Asia"
```

### Create a Secret
```
az keyvault secret set --vault-name "SD1597" --name "App-ConnectionString" --value "MySecret"
```
### Get Secret
```
az keyvault secret show --name "App-ConnectionString" --vault-name "SD1597"
```

### Create an Application named "TestApp" with Client Secret as "AppSecret"
```
az ad app create --display-name "TestApp" --password "AppSecret" --credential-description "TestAppSecret"
```

### Get the ApplicationId of the newly created Application
```
az ad app list --display-name "TestApp" --query [].appId -o tsv
```

### Create a Service Principal for the ApplicationId above
```
az ad sp create --id "5f068843-8836-4644-8e45-02a7626a78ee"
```

### Grab the PrincipalId
```
az ad sp list --display-name "TestApp" --query [].objectId -o tsv
```

### Assign permissions for the Service Principal in the Key Vault
```
az keyvault set-policy --name "SD1597" --object-id "c6708856-5382-4573-a9fb-264df2032176" --secret-permissions get list
```

### Install Nuget Packages
```
Microsoft.Azure.KeyVault
Microsoft.Azure.Services.AppAuthentication
```

### Access Secrets from C# Application
```c#
static async Task Main(string[] args)
{
    try
    {
        KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessTokenAsync));
        var secret = await keyVaultClient.GetSecretAsync("https://SD1597.vault.azure.net/secrets/App-ConnectionString").ConfigureAwait(false);

        Console.WriteLine(secret.Value);
    }
    catch (KeyVaultErrorException keyVaultException)
    {
        Console.WriteLine(keyVaultException.Message);
    }

    Console.ReadLine();
}

private static async Task<string> GetAccessTokenAsync(string authority, string resource, string scope)
{
    var appCredentials = new ClientCredential("5f068843-8836-4644-8e45-02a7626a78ee", "AppSecret");
    var context = new AuthenticationContext(authority, TokenCache.DefaultShared);

    var result = await context.AcquireTokenAsync(resource, appCredentials);

    return result.AccessToken;
}
```
