using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class QuestionCommentRepository : GenericRepository<QuestionComment>, IQuestionCommentRepository
{
    public QuestionCommentRepository(FitnessContext context) : base(context)
    {
    }
}