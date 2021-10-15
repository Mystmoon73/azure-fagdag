# WebApp, API, Service Bus, SignalR og Azure Functions

Denne branchen fortsetter fra forrige. I denne branchen brukes Azure Service Bus, SignalR
Azure Functions (time, http, and service bus triggere) samt Azure Table Storage

### Azure konfigurasjon

### Portal

Logg inn i [Azure portal](https://portal.azure.com/#home). Forutsetter at
ressursgruppe og ressurser fra **01-webapp-and-webapi** er opprettet. Hvis ikke se README.

For navngiving se [her](https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations)

- Opprett Azure SignalR Service (serverless)
- **sigr-`<navn>`**
  ![Signalr](signalr.png)
- Opprett Storage account
  - **st`<navn>`** (merk ikke tillatt med "-")
    ![Stb](stb.png)
- Opprett Service Bus ressurser

  - Namespace:
    - **sb-`<navn>`**
      ![Sb](sb.png)
  - Queue(flight-notifications-queue):
    ![queue](queue.png)

- Opprett Function App (**Hosting**: velg storage account-en som allerede er opprettet. **Monitoring**: velg Application insight som er oppprett tidligere. Velg App Service plan som ble opprettet tidligere)
  - **func-`<navn>`**
    ![func](func.png)

Dette gir da følgende innhold i ressurs-gruppen:

![Ressursgruppeinnhold](resource-group.png)

## Key-vault

Legge inn connection strings fra SignalR, Storage Account (Access keys) og Service Bus (shared access key. Kan gjerne lag ny med begrensede rettighter) i Key-vault og gi dem følgende navn (secrets):

- AzureSignalRConnectionString
- AzureWebJobsStorage
- ServiceBus--ConnectionString
- ServiceBus--QueueName

### Konfigurere Key-vault for Function App

#### Key-vault

- Under "Access policies" -> Legg til ny (Se branch 01 for hvordan dette gjøres). Søk opp navn på func eller eller benytte service principal id

### Function App

Legg til Key-vault referanse i **Application Settings** underer **Configuration**:

- AzureSignalRConnectionString
- AzureWebJobsStorage
- AzureWebJobsServiceBus (benytt ServiceBus--ConnectionString)
- QueueName benytt (ServiceBus--QueueName)

For hver av dem referer til Key-vault slik:

```
@Microsoft.KeyVault(VaultName=myvault;SecretName=mysecret)
```

Dersom func-en har rettigheter til å lese fra key-vauylt vil det se slik ut
![Config](func-config.png)

## Deploy kode

### Visual studio 2019

Benytt publish både på "Flight.API" og så på "Notification".
