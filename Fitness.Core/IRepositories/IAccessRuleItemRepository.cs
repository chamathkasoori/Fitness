using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IAccessRuleItemRepository : IGenericRepository<AccessRuleItem>
{
    public Task<List<AccessRuleItem>> GetAllIsActiveForNewClubsAsync();

    public Task SaveAssignedClubsAsync(IEnumerable<AccessRuleItemAssignedClub> items);
}
