using Azure.Communication.Sms;

namespace SynchronousConsoleClient;

/// <summary>
/// A client for sending SMS messages using Azure Communication Services
/// </summary>
public class AzureCommunicationServiceSmsClient(AzureCommunicationServiceOptions azureCommunicationServiceOptions)
{
    private readonly SmsClient _client = new(azureCommunicationServiceOptions.PrimaryConnectionString);

    /// <summary>
    /// Sends an SMS message synchronously to another US-based phone number
    /// </summary>
    /// <param name="phoneNumber">The US-based phone number to send the SMS message to</param>
    /// <param name="message">The message sent to the phone number</param>
    public async Task SendMessageToPhoneNumber(string phoneNumber, string message)
    {
        try
        {
            SmsSendResult sendResult = await _client.SendAsync(
                from: azureCommunicationServiceOptions.PhoneNumber, // Your E.164 formatted from phone number used to send SMS
                to: phoneNumber, // E.164 formatted recipient phone number
                message: message);
            Console.WriteLine($"Sms id: {sendResult.MessageId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}