using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IQuestionAnswerService : IGenericService<QuestionAnswer>
{
    Task<IReadOnlyList<QuestionAnswer>> GetAllByMemberAsync(int memberId);
    
    Task<IReadOnlyList<Member>> GetAllDistinctMembersAsync();

    Task SaveAsync(IEnumerable<QuestionAnswer> list);
}