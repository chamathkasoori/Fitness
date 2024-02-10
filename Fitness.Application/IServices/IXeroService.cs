using Fitness.Core.Common.Xero.Contact;
using Fitness.Core.Common.Xero.TrackingCategory;
using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IXeroService
{
    Task<XeroResponseContactsDto> GetContact(string contactName);

    Task<XeroResponseTrackingCategoryDto> GetTrackingCategory(string name);

    Task<XeroTrackingCategoryDto> CreateTrackingCategoryIfNotExistAsync(Member member);

    Task<XeroContactDto> CreateContactIfNotExistAsync(Member member);
    
    Task SaveToken(Config configs);

    Task<string> MakeAccountingPutRequest(string resource, object body);
}