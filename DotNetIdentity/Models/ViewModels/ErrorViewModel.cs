namespace DotNetIdentity.Models.ViewModels {
/// <summary>
/// model for displaying errors
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// the rquest id
    /// </summary>
    /// <value>string</value>
    public string? RequestId { get; set; }
    /// <summary>
    /// show the requst id
    /// </summary>
    /// <returns>bool</returns>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
}