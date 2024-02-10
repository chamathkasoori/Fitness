using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IQuestionAnswerRepository : IGenericRepository<QuestionAnswer>
{
    Task<IReadOnlyList<QuestionAnswer>> GetAllByMemberAsync(int memberId);

    Task<IReadOnlyList<Member>> GetAllDistinctMembersAsync();

    Task SaveAsync(IEnumerable<QuestionAnswer> items);
}