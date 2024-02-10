using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fitness.Controllers;
public class QuestionsController : CommonController<QuestionsController>
{
    private readonly IMapper _mapper;
    private readonly IQuestionService _questionService;
    private readonly IQuestionMultiLanguageService _questionMultiLanguageService;
    private readonly IQuestionAnswerService _questionAnswerService;
    private readonly IQuestionCommentService _questionCommentService;
    
    public QuestionsController(IQuestionService questionService, IQuestionMultiLanguageService questionMultiLanguageService, IQuestionAnswerService questionAnswerService, IMapper mapper, IQuestionCommentService questionCommentService)
    {
        _questionService = questionService;
        _questionMultiLanguageService = questionMultiLanguageService;
        _questionAnswerService = questionAnswerService;
        _mapper = mapper;
        _questionCommentService = questionCommentService;
    }

    [AllowAnonymous]
    [HttpGet("Languages")]
    public IActionResult GetLanguages()
    {
        var items = Enum.GetValues(typeof(Languages)).Cast<Languages>().Select(x => new { Id = (int)x, Name = x.ToString() });
        return Ok(items);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetQuestionsAsync(Languages? language)
    {
        if (language.HasValue && language.Value != Languages.EN)
        {
            var questions = _mapper.Map<List<QuestionDto>>(await _questionMultiLanguageService.GetAllByLanguageAsync(language.Value));
            return Ok(questions);
        }
        else
        {
            var defaultQuestions = _mapper.Map<List<QuestionDto>>(await _questionService.GetAllAsync());
            return Ok(defaultQuestions);
        }
    }

    [HttpGet("Answers/Members/{memberId:int}")]
    public async Task<IActionResult> GetQuestionAnswersByMember(int memberId)
    {
        var items = await _questionAnswerService.GetAllByMemberAsync(memberId);
        return Ok(_mapper.Map<IReadOnlyList<QuestionAnswerDto>>(items));
    }

    [HttpGet("Answers/Members")]
    public async Task<IActionResult> GetDistinctMembersAsync()
    {
        var members = _mapper.Map<List<MemberDto>>(await _questionAnswerService.GetAllDistinctMembersAsync());
        return Ok(members);
    }

    [AllowAnonymous]
    [HttpPost("Answers")]
    public async Task<IActionResult> SaveQuestionAnswersAsync([FromBody] QuestionAnswerPostDto questionAnswers, [FromQuery] int memberId)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var items = _mapper.Map<List<QuestionAnswer>>(questionAnswers.Answers);
        foreach (var item in items)
        {
            item.MemberId = memberId;
            item.ModifiedBy = access!.UserId;
            item.ModifiedOn = DateTime.UtcNow;
        }

        if (questionAnswers.Comment != null)
        {
            var questionComment = new QuestionComment
            {
                MemberId = memberId,
                Comment = questionAnswers.Comment,
                CreatedBy = access!.UserId,
            };
            
            await _questionCommentService.AddAsync(questionComment);
        }

        await _questionAnswerService.SaveAsync(items);
        return CreatedAtAction("GetQuestions", questionAnswers);
    }
}