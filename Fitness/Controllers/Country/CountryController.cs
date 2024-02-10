using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class CountryController : CommonController<CountryController>
{
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;
    public CountryController(IMapper mapper, ICountryService countryService)
    {
        _mapper = mapper;
        _countryService = countryService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyList<CountryDto>> GetAllAsync()
    {
        var countrys = await _countryService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CountryDto>>(countrys);
    }
}