using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class PaymentIntervalPostDto
{
    public string Name { get; set; } = string.Empty;

    public int CompanyId { get; set; }

    public PaymentType Type { get; set; }

    public int Value { get; set; }
}
