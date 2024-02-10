using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class QuestionCommentService : IQuestionCommentService
{
    private readonly IQuestionCommentRepository _questionCommentRepository;

    public QuestionCommentService(IQuestionCommentRepository questionCommentRepository)
    {
        _questionCommentRepository = questionCommentRepository;
    }

    public async Task<IReadOnlyList<QuestionComment>> GetAllAsync()
    {
        return await _questionCommentRepository.GetAllAsync();
    }

    public async Task<QuestionComment?> GetByIdAsync(int id)
    {
        return await _questionCommentRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(QuestionComment entity)
    {
        await _questionCommentRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(QuestionComment entity)
    {
        await _questionCommentRepository.UpdateAsync(entity);
    }
}