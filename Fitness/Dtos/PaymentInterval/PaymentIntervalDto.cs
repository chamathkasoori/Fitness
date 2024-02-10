using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class PaymentIntervalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public PaymentType Type { get; set; }
    public int Value { get; set; }
}
