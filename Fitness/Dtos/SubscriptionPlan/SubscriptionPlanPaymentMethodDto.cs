namespace Fitness.Dtos;
public class SubscriptionPlanPaymentMethodDto
{
    public int SubscriptionPlanId { get; set; }

    public int PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = string.Empty;
}
