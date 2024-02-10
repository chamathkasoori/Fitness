using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class QuestionAnswerService : IQuestionAnswerService
{
    private readonly IQuestionAnswerRepository _questionAnswerRepository;
    public QuestionAnswerService(IQuestionAnswerRepository questionAnswerRepository)
    {
        _questionAnswerRepository = questionAnswerRepository;
    }

    Task<IReadOnlyList<QuestionAnswer>> IGenericService<QuestionAnswer>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<QuestionAnswer>> GetAllByMemberAsync(int memberId)
    {
        return await _questionAnswerRepository.GetAllByMemberAsync(memberId);
    }

    public async Task<IReadOnlyList<Member>> GetAllDistinctMembersAsync()
    {
        return await _questionAnswerRepository.GetAllDistinctMembersAsync();
    }

    public Task<QuestionAnswer?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(IEnumerable<QuestionAnswer> list)
    {
        return _questionAnswerRepository.SaveAsync(list);
    }

    public Task AddAsync(QuestionAnswer entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(QuestionAnswer entity)
    {
        throw new NotImplementedException();
    }
}