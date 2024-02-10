using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class InquiryReplyService : IInquiryReplyService
{
    private readonly IInquiryReplyRepository _inquiryReplyRepository;
    private readonly IInquiryService _inquiryService;
    public InquiryReplyService(IInquiryReplyRepository inquiryReplyRepository, IInquiryService inquiryService)
    {
        _inquiryReplyRepository = inquiryReplyRepository;
        _inquiryService = inquiryService;
    }

    public async Task<IReadOnlyList<InquiryReply>> GetAllAsync()
    {
        return await _inquiryReplyRepository.GetAllAsync();
    }

    public async Task<InquiryReply?> GetByIdAsync(int id)
    {
        return await _inquiryReplyRepository.GetByIdAsync(id);
    }
    
    public async Task<InquiryReply?> GetByInquiryIdAsync(int id)
    {
        return await _inquiryReplyRepository.GetByInquiryIdAsync(id);
    }

    public async Task AddAsync(InquiryReply entity)
    {
        await _inquiryReplyRepository.AddAsync(entity); 
    }

    public async Task UpdateAsync(InquiryReply entity)
    {
        await _inquiryReplyRepository.UpdateAsync(entity);
    }
}