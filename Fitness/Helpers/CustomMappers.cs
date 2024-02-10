using Fitness.Core.Entities;
using Fitness.Dtos;

namespace Fitness.Helpers;

public static class CustomMappers
{

    public static List<MemberTransactionDto> MapTransactions(MemberSubscription memberSubscription)
        => memberSubscription.MemberSubscriptionTransactions.Select(x => new MemberTransactionDto()
        {
            Id = x.Id,
            Description = x.Description,
            Amount = x.Amount,
            OperationType = (int)x.OperationType,
            PaymentMethod = x.PaymentMethod,
            TransactionCategory = x.TransactionCategory,
            TransactionDate = x.TransactionDate,
            TransactionType = x.TransactionType,
            VatPercentage = x.VatPercentage,
            CreatedOn = x.CreatedOn,
            Status = memberSubscription
                    .Invoices
                    .OrderByDescending(a => a.UpdatedDateUTC)
                    .First()
                    .Status == "AUTHORISED" ? "Paid" : "Unpaid"
        }).ToList();
}
