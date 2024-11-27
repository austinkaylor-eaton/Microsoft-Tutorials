# [Azure Communication SMS samples for .NET](https://learn.microsoft.com/en-us/samples/azure/azure-sdk-for-net/azure-communication-sms-sdk-samples/?tab=tab-created&ns-enrollment-type=Collection&ns-enrollment-id=kkejt58wpq5628)
# Setup
1. Create or use an existing Azure subscription
2. Create a [Communication Services](https://portal.azure.com/#view/Microsoft_Azure_Marketplace/GalleryItemDetailsBladeNopdl/id/Microsoft.Communication/selectionMode~/false/resourceGroupId//resourceGroupLocation//dontDiscardJourney~/false/selectedMenuId/home/launchingContext~/%7B%22galleryItemId%22%3A%22Microsoft.Communication%22%2C%22source%22%3A%5B%22GalleryFeaturedMenuItemPart%22%2C%22VirtualizedTileDetails%22%5D%2C%22menuItemId%22%3A%22home%22%2C%22subMenuItemId%22%3A%22Search%20results%22%2C%22telemetryId%22%3A%2224abf256-b591-4bb1-aa11-08c28de99d8f%22%7D/searchTelemetryId/e5019623-f008-45ca-876c-b96025e2d4fa) resource
3. Follow the [Quickstart: Get and manage phone numbers](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/telephony/get-phone-number?tabs=windows&pivots=platform-azp) to create a phone number to send SMS messages
4. Clone the repository

# Projects
## [AsyncConsoleApp](./AsyncConsoleApp)

## [SynchronousConsoleApp](./SynchronousConsoleApp)
This sample demonstrates how to send an SMS message to an individual or a group of recipients.

This sample sends SMS messages synchronously between two phone numbers.

# Running each project
1. Open the project an IDE of your choice (Visual Studio, Visual Studio Code, Rider, etc.)
2. Each project uses [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows#secret-manager). Update the `secrets.json` file for each project with the following values:
    ```json
    {
      "AzureCommunicationServices": {
        "ServiceEndpoint": "<azure communication service endpoint>",
        "PrimaryKey": "<azure communication service primary key>",
        "PrimaryConnectionString": "<azure communication service primary connection string>"
      }
    }
    ```
3. Clean and build
4. Run the project

# Troubleshooting

# Further Reading
- [Azure Communication Services SMS documentation](https://learn.microsoft.com/en-us/azure/communication-services/concepts/sms/concepts)
- [Azure Communication Services .NET SDK documentation](https://learn.microsoft.com/en-us/dotnet/azure/sdk/communication-chat/overview)
- [Azure Communication Services Pricing](https://learn.microsoft.com/en-us/azure/communication-services/concepts/pricing)
- [Azure Communication Services samples](https://learn.microsoft.com/en-us/samples/azure/azure-sdk-for-net/azure-communication-sms-sdk-samples/?tab=tab-created&ns-enrollment-type=Collection&ns-enrollment-id=kkejt58wpq5628)
