using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IInquiryService : IGenericService<Inquiry>
{
    Task<IReadOnlyList<Inquiry>> GetAllByClubsAsync(List<int> ClubIdList, string type);

    Task<IReadOnlyList<Inquiry>> GetAllByMemberAsync(int memberId);
}