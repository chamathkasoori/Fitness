using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Core.Entities;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class TagsController : CommonController<TagsController>
{
    private readonly IMapper mapper;
    private readonly ITagService tagService;
    public TagsController(IMapper mapper, ITagService tagService)
    {
        this.mapper = mapper;
        this.tagService = tagService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<TagDto>> GetAllAsync()
    {
        var items = await tagService.GetAllAsync();
        return mapper.Map<IReadOnlyList<TagDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TagPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<Tag>(model);
        item.CreatedBy = access!.UserId;

        await tagService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TagPostDto model)
    {
        var extItem = await tagService.GetByIdAsync(id);
        if (extItem == null)
        {
            return BadRequest("Tag Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<Tag>(model);
        item.Id = id;
        item.CreatedBy = extItem.CreatedBy;
        item.CreatedOn = extItem.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        await tagService.UpdateAsync(item);
        return NoContent();
    }
}
