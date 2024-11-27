using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SynchronousConsoleClient;

/// <summary>
/// Options for connecting to and using an <a href="">Azure Communication Service</a>.
/// </summary>
/// <remarks>Implements the <a href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options">Options pattern</a><br></br></remarks>
/// <remarks></remarks>
public sealed class AzureCommunicationServiceOptions
{
    private const string ConnectionStringRegEx = @"^endpoint=https:\/\/[^.]+\.unitedstates\.communication\.azure\.com\/;accesskey=[A-Za-z0-9]{84}$";
    private const string ServiceEndpointRegEx = @"^https:\/\/[^.]+\.unitedstates\.communication\.azure\.com\/$";
    private const string E164PhoneNumberRegEx = @"^\+?[1-9]\d{1,14}$";
    public static readonly string ConfigurationSectionName = nameof(AzureCommunicationServiceOptions).AsSpan(nameof(AzureCommunicationServiceOptions).Length - "Options".Length).ToString();

    /// <summary>
    /// The service endpoint for the Azure Communication Service.
    /// </summary>
    /// <example>https://azure-resource-name.unitedstates.communication.azure.com/</example>
    [Required]
    [RegularExpression(ServiceEndpointRegEx)]
    public string ServiceEndpoint { get; set; }
    
    /// <summary>
    /// The Primary Key for the Azure Communication Service.
    /// </summary>
    /// <example>7AD4CaDdI6kZ1f34lOJvRIz71orYVJHNzQd0uWqwE4fRrQcSMLOOJQQJ99AKACULyCpdHlM5AAAAAZCSXbZd</example>
    [Required]
    public string PrimaryKey { get; set; }
    
    /// <summary>
    /// The Primary Connection String for the Azure Communication Service.
    /// </summary>
    /// <example></example>
    /// <remarks>This is a combination of the <see cref="ServiceEndpoint"/> and the <see cref="PrimaryKey"/></remarks>
    [Required]
    [RegularExpression(ConnectionStringRegEx)]
    public string PrimaryConnectionString { get; set; }
    
    /// <summary>
    /// Represents the <a href="https://www.itu.int/rec/T-REC-E.164/en">E.164</a> compliant phone number to send the SMS message from.
    /// </summary>
    [Required]
    [RegularExpression(E164PhoneNumberRegEx, ErrorMessage = "The phone number must be in E.164 format.")]
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// The Secondary Key for the Azure Communication Service.
    /// </summary>
    /// <example>7AD4CaDdI6kZ1f34lOJvRIz71orYVJHNzQd0uWqwE4fRrQcSMLOOJQQJ99AKACULyCpdHlM5AAAAAZCSXbZd</example>
    public string SecondaryKey { get; set; }
    
    /// <summary>
    /// The Secondary Connection String for the Azure Communication Service.
    /// </summary>
    /// <example>endpoint=https://azure-resource-name.unitedstates.communication.azure.com/;accesskey=7AD4CaDdI6kZ1f34lOJvRIz71orYVJHNzQd0uWqwE4fRrQcSMLOOJQQJ99AKACULyCpdHlM5AAAAAZCSXbZd</example>
    /// <remarks>This is a combination of the <see cref="ServiceEndpoint"/> and the <see cref="SecondaryKey"/></remarks>
    [RegularExpression(ConnectionStringRegEx)]
    public string SecondaryConnectionString { get; set; }
}