using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    public QuestionRepository(FitnessContext context) : base(context)
    {
    }
}