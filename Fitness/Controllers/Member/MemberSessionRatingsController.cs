using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
[AllowAnonymous]
public class MemberSessionRatingsController : CommonController<MemberSessionRatingsController>
{
    private readonly IMapper _mapper;
    private readonly IMemberSessionRatingService _memberSessionRatingService;
    public MemberSessionRatingsController(IMemberSessionRatingService membseSessionRatingService, IMapper mapper)
    {
        _mapper = mapper;
        _memberSessionRatingService = membseSessionRatingService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<MemberSessionRatingDto>> GetAllAsync()
    {
        var items = await _memberSessionRatingService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<MemberSessionRatingDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(MemberSessionRatingPostDto model)
    {
        var membseSessionRating = _mapper.Map<MemberSessionRating>(model);
        await _memberSessionRatingService.AddAsync(membseSessionRating);

        return CreatedAtAction("GetAll", new { id = membseSessionRating.Id }, membseSessionRating);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, MemberSessionRatingPostDto model)
    {
        var membseSessionRating = _mapper.Map<MemberSessionRating>(model);
        membseSessionRating.Id = id;
        await _memberSessionRatingService.UpdateAsync(membseSessionRating);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _memberSessionRatingService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("MemberSessionRating Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _memberSessionRatingService.UpdateAsync(item);
        return NoContent();
    }
}