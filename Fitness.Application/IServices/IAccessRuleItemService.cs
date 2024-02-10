using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IAccessRuleItemService : IGenericService<AccessRuleItem>
{
    public Task AddNewClubAsync(int clubId, int createdBy);
}
