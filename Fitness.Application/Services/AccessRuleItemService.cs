using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class AccessRuleItemService : IAccessRuleItemService
{
    private readonly IAccessRuleItemRepository accessRuleItemRepository;
    public AccessRuleItemService(IAccessRuleItemRepository accessRuleItemRepository)
    {
        this.accessRuleItemRepository = accessRuleItemRepository;
    }

    async Task<IReadOnlyList<AccessRuleItem>> IGenericService<AccessRuleItem>.GetAllAsync()
    {
        return await accessRuleItemRepository.GetAllAsync();
    }

    async Task<AccessRuleItem?> IGenericService<AccessRuleItem>.GetByIdAsync(int id)
    {
        return await accessRuleItemRepository.GetByIdAsync(id);
    }

    async Task IGenericService<AccessRuleItem>.AddAsync(AccessRuleItem entity)
    {
        await accessRuleItemRepository.AddAsync(entity);
    }

    async Task IGenericService<AccessRuleItem>.UpdateAsync(AccessRuleItem entity)
    {
        await accessRuleItemRepository.UpdateAsync(entity);
    }

    async Task IAccessRuleItemService.AddNewClubAsync(int clubId, int createdBy)
    {
        var accessRuleItemList = await accessRuleItemRepository.GetAllIsActiveForNewClubsAsync();
        if (accessRuleItemList.Any())
        {
            List<AccessRuleItemAssignedClub> items = new List<AccessRuleItemAssignedClub>();
            foreach (var accessRuleItem in accessRuleItemList)
            {
                AccessRuleItemAssignedClub item = new AccessRuleItemAssignedClub();
                item.AccessRuleItemId = accessRuleItem.Id;
                item.ClubId = clubId;
                item.CreatedBy = createdBy;
                items.Add(item);
            }
            await accessRuleItemRepository.SaveAssignedClubsAsync(items);
        }
    }
}
