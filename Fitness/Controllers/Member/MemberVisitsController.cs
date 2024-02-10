using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class MemberVisitsController : CommonController<MemberVisitsController>
{
    private readonly IMapper _mapper;
    private readonly IMemberVisitService _memberVisitService;
    private readonly IMemberSessionRatingService _memberSessionRatingService;
    public MemberVisitsController(IMapper mapper, IMemberVisitService membseVisitService, IMemberSessionRatingService memberSessionRatingService)
    {
        _mapper = mapper;
        _memberVisitService = membseVisitService;
        _memberSessionRatingService = memberSessionRatingService;
    }

    [HttpGet("member/{memberId:int}/club/{club:int}/size/{size:int}")]
    public async Task<IActionResult> GetAll(int memberId, int clubId, int size = 50)
    {
        var visits = await _memberVisitService.GetAllAsync(memberId, clubId, size);
        var ratings = await _memberSessionRatingService.GetAllByVisitsAsync(visits.Select(x => x.Id).ToList());

        var items = _mapper.Map<IReadOnlyList<MemberVisitDto>>(visits);
        foreach (var item in items)
        {
            item.MemberFeedbackGiven = ratings.Any(x => x.VisitId == item.Id);
        }
        return Ok(items);
    }
}
