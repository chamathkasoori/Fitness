using Fitness.Core.Common.Xero.Core;

namespace Fitness.Core.Common.Xero.Contact;
public class XeroContactDto : XeroResponseDto
{
    public Guid ContactID { get; set; }
    public string ContactStatus { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string BankAccountDetails { get; set; } = string.Empty;
    public DateTime UpdatedDateUTC { get; set; }
    public bool IsSupplier { get; set; }
    public bool IsCustomer { get; set; }
    public bool HasValidationErrors { get; set; }
}

public class XeroCreateContactDto
{
    public string Name { get; set; } = string.Empty;
}
