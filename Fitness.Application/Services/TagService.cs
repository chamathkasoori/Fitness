using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class TagService : ITagService
{
    private readonly ITagRepository tagRepository;
    public TagService(ITagRepository TagRepository)
    {
        tagRepository = TagRepository;
    }

    async Task<IReadOnlyList<Tag>> IGenericService<Tag>.GetAllAsync()
    {
        return await tagRepository.GetAllAsync();
    }

    async Task<Tag?> IGenericService<Tag>.GetByIdAsync(int id)
    {
        return await tagRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Tag>.AddAsync(Tag entity)
    {
        await tagRepository.AddAsync(entity);
    }

    async Task IGenericService<Tag>.UpdateAsync(Tag entity)
    {
        await tagRepository.UpdateAsync(entity);
    }
}