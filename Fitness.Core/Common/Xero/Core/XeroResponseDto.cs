namespace Fitness.Core.Common.Xero.Core;
public class XeroResponseDto
{
    public Guid Id { get; set; }
    public string? Status { get; set; }
    public string? ProviderName { get; set; }
    public DateTime DateTimeUTC { get; set; }
}