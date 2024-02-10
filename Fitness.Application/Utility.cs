using Fitness.Core.Entities;
using Fitness.Core.Enums;

namespace Fitness.Application;
public class Utility
{
    public static DateTime GetEndDate(DateTime startDate, PaymentInterval paymentInterval)
    {
        if (paymentInterval.Type == PaymentType.Day)
        {
            return startDate.AddDays(paymentInterval.Value).AddSeconds(-1);
        }
        else if (paymentInterval.Type == PaymentType.Week)
        {
            return startDate.AddDays(paymentInterval.Value * 7).AddSeconds(-1);
        }
        else if (paymentInterval.Type == PaymentType.Month)
        {
            return startDate.AddMonths(paymentInterval.Value).AddSeconds(-1);
        }
        else if (paymentInterval.Type == PaymentType.Year)
        {
            return startDate.AddYears(paymentInterval.Value).AddSeconds(-1);
        }
        else
        {
            return startDate;
        }
    }
}
