using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class QuestionMultiLanguageRepository : GenericRepository<QuestionMultiLanguage>, IQuestionMultiLanguageRepository
{
    private readonly FitnessContext _context;
    public QuestionMultiLanguageRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<QuestionMultiLanguage>> GetAllByLanguageAsync(Languages language)
    {
        return await _context.QuestionMultiLanguages
            .Where(x => !x.IsDelete && x.Language == language)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}