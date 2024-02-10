using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;
using Fitness.Core.Common.Zendesk.Ticket;
using Microsoft.Extensions.Configuration;

namespace Fitness.Application.Services;
public class InquiryService : IInquiryService
{
    private readonly IInquiryRepository inquiryRepository;
    private readonly IConfiguration configuration;
    private readonly IMemberService memberService;
    private readonly IZendeskService zendeskService;
    public InquiryService(IInquiryRepository inquiryRepository, IConfiguration configuration, IMemberService memberService, IZendeskService zendeskService)
    {
        this.inquiryRepository = inquiryRepository;
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.memberService = memberService;
        this.zendeskService = zendeskService;
    }

    public async Task<IReadOnlyList<Inquiry>> GetAllAsync()
    {
        return await inquiryRepository.GetAllAsync();
    }

    public async Task<IReadOnlyList<Inquiry>> GetAllByClubsAsync(List<int> clubIdList, string type)
    {
        return await inquiryRepository.GetAllByClubsAsync(clubIdList, type);
    }

    public async Task<IReadOnlyList<Inquiry>> GetAllByMemberAsync(int memberId)
    {
        return await inquiryRepository.GetAllByMemberAsync(memberId);
    }
    
    public async Task<Inquiry?> GetByIdAsync(int id)
    {
        return await inquiryRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Inquiry entity)
    {
        var member = await memberService.GetByIdAsync(entity.MemberId);
        TicketDto zendeskTicketDto = new TicketDto
        {
            Ticket = new Ticket
            {
                Comment = new Comment
                {
                    Body = entity.InquiryEntry
                },
                Priority = "urgent",
                Subject = $"Ticket from {member!.User!.FirstName} {member.User.LastName} of {member.Club.Name}",
                ExternalId = Convert.ToInt32(entity.TicketId),
                Requester = new RequesterDetails { Email = member.User.Email, Name = $"{member!.User!.FirstName} {member.User.LastName}" },
                CustomFields = new CustomField[]
                {
                    new CustomField { Id = Convert.ToInt64(configuration["Zendesk:CustomFieldIds:PhoneNumber"]), Value = Convert.ToString(member.User.PhoneNumber) }
                }
            }
        };
        await inquiryRepository.AddAsync(entity);

        //Zendesk Ticket Creation
        await zendeskService.SendRequestAsync(configuration["Zendesk:Paths:Ticket"] ?? String.Empty, RestSharp.Method.Post, zendeskTicketDto);
    }

    public async Task UpdateAsync(Inquiry entity)
    {
        await inquiryRepository.UpdateAsync(entity);
    }
}