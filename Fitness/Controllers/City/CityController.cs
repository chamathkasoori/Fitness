using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers.City;
[AllowAnonymous]
public class CityController : CommonController<CityController>
{
    private readonly IMapper _mapper;
    private readonly ICityService _cityService;
    public CityController(IMapper mapper, ICityService cityService)
    {
        _mapper = mapper;
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<CityDto>> GetAllAsync()
    {
        var citys = await _cityService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CityDto>>(citys);
    }

    [HttpGet("{countryId:int}")]
    public async Task<IReadOnlyList<CityDto>> GetAllByCountryId(int countryId)
    {
        var citys = await _cityService.GetAllByCountryIdAsync(countryId);
        return _mapper.Map<IReadOnlyList<CityDto>>(citys);
    }
}