using AutoMapper;
using Fitness.Core.Common.Xero;
using Fitness.Core.Common.Xero.Contact;
using Fitness.Core.Common.Xero.TrackingCategory;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Fitness.Application.Services;

public class XeroService : IXeroService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ISystemConfigService _systemConfigService;
    private string accessToken = string.Empty;
    
    public XeroService(IConfiguration configuration, IMapper mapper, ISystemConfigService systemConfigService)
    {
        _configuration = configuration;
        _mapper = mapper;
        _systemConfigService = systemConfigService;
    }

    public async Task SaveToken(Config configs)
    {
        try
        {
            var data = await _systemConfigService.GetFirstAsync();
            if (data == null)
            {
                await _systemConfigService.AddAsync(new SystemConfig{Configs = _mapper.Map<Config>(configs)});
            }
            else
            {
                data.Configs.XeroToken = configs.XeroToken;
                await _systemConfigService.UpdateAsync(data);
            }
        }
        catch (Exception e)
        {
            throw new Exception("Failed to Save Token: " + e.Message );
        }
    }

    public async Task<XeroResponseContactsDto> GetContact(string contactName)
    {
        accessToken = string.IsNullOrEmpty(accessToken) ? await GetAccessToken() : accessToken;
        var xeroTenantId = _configuration["Xero:TenantId"] ?? string.Empty;
            
        var options = new RestClientOptions(_configuration["Xero:AccountingUrl"] ?? string.Empty)
        {
            Authenticator = new JwtAuthenticator(accessToken),
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest("Contacts");

        request.AddHeader("Xero-Tenant-Id", xeroTenantId);
        request.AddQueryParameter("where", $"Name=\"{contactName}\"");

        var response = await client.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<XeroResponseContactsDto>(response.Content ?? "{}") ?? new XeroResponseContactsDto();
    }

    public async Task<XeroResponseTrackingCategoryDto> GetTrackingCategory(string name)
    {
        accessToken = string.IsNullOrEmpty(accessToken) ? await GetAccessToken() : accessToken;
        var xeroTenantId = _configuration["Xero:TenantId"] ?? string.Empty;
            
        var options = new RestClientOptions(_configuration["Xero:AccountingUrl"] ?? string.Empty)
        {
            Authenticator = new JwtAuthenticator(accessToken),
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest("TrackingCategories");

        request.AddHeader("Xero-Tenant-Id", xeroTenantId);
        request.AddQueryParameter("where", $"Name=\"{name}\"");

        var response = await client.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<XeroResponseTrackingCategoryDto>(response.Content ?? "{}") ?? new XeroResponseTrackingCategoryDto();
    }
    
    public async Task<string> MakeAccountingPutRequest(string resource, object body)
    {
        accessToken = string.IsNullOrEmpty(accessToken) ? await GetAccessToken() : accessToken;
        var xeroTenantId = _configuration["Xero:TenantId"] ?? string.Empty;
            
        var options = new RestClientOptions(_configuration["Xero:AccountingUrl"] ?? string.Empty)
        {
            Authenticator = new JwtAuthenticator(accessToken),
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest(resource)
        {
            Method = Method.Put
        };

        request.AddHeader("Xero-Tenant-Id", xeroTenantId);
        request.AddBody(body);

        var response = await client.ExecuteAsync(request);

        return response.Content ?? "{}";
    }

    public async Task<XeroTrackingCategoryDto> CreateTrackingCategoryIfNotExistAsync(Member member)
    {
        var category = await GetTrackingCategory(_configuration["Xero:TrackingCategory"] ?? "Branch");

        if (category.TrackingCategories.Count != 0)
        {
            if (category.TrackingCategories[0].Options.Any(t => t.Name == member.Club.Name.Split("-")[0].Trim()))
                return category.TrackingCategories[0];
            
            var optionResponse = await CreateTrackingCategoryOption(category.TrackingCategories[0].TrackingCategoryID, member);
            return optionResponse.TrackingCategories[0];
        }

        var response = await CreateTrackingCategory();
        var newOption = await CreateTrackingCategoryOption(response.TrackingCategories[0].TrackingCategoryID, member);
        return newOption.TrackingCategories[0];
    }

    public async Task<XeroContactDto> CreateContactIfNotExistAsync(Member member)
    {
        try
        {
            var contact = await GetContact(member.Club.Name);

            if (contact.Contacts.Count != 0) 
                return _mapper.Map<XeroContactDto>(contact.Contacts[0]);
            
            var response = await CreateContact(member);
            return _mapper.Map<XeroContactDto>(response.Contacts[0]);
        }
        catch (Exception ex)
        {
            throw new Exception("CreateContactIfNotExistAsync: " + ex.Message);
        }
    }

    private async Task<string> GetAccessToken()
    {
        try
        {
            var config = await _systemConfigService.GetFirstAsync();
            if (config == null) throw new Exception("Not Found");
            
            var options = new RestClientOptions(_configuration["Xero:BaseAuthUrl"] ?? string.Empty)
            {
                Authenticator = new HttpBasicAuthenticator(_configuration["Xero:ClientId"] ?? string.Empty, _configuration["Xero:ClientSecret"] ?? string.Empty),
                ThrowOnAnyError = true
            };
            var client = new RestClient(options);
            
            var request = new RestRequest($"{_configuration["Xero:TokenUrl"]}")
            {
                Method = Method.Post
            };

            request.AddParameter("grant_type", _configuration["Xero:GrantType"]);
            request.AddParameter("refresh_token", config.Configs.XeroToken);
            request.AddParameter("scope", _configuration["Xero:Scope"]);
            
            var response = await client.ExecuteAsync(request);
            
            var auth = JsonConvert.DeserializeObject<XeroAuthDto>(response.Content ?? "{}");
            if (auth == null)
                throw new Exception("Deserialize Error");
                
            config.Configs.XeroToken = auth.RefreshToken;
            await _systemConfigService.UpdateAsync(config);
            
            return auth.AccessToken;
        }
        catch (Exception e)
        {
            throw new Exception("Failed To Get Access Token: " + e.Message);
        }
    }

    private async Task<XeroResponseTrackingCategoryDto> CreateTrackingCategory()
    {
        var categoryDto = new XeroCreateTrackingCategoryDto { Name = _configuration["Xero:TrackingCategory"] ?? "Branch"};
        
        var response = await MakeAccountingPutRequest("TrackingCategories", categoryDto);
        
        return JsonConvert.DeserializeObject<XeroResponseTrackingCategoryDto>(response) ?? new XeroResponseTrackingCategoryDto();
    }
    
    private async Task<XeroResponseTrackingCategoryDto> CreateTrackingCategoryOption(Guid trackingId, Member member)
    {
        var option = new XeroCreateTrackingCategoryOptionDto { Name = member.Club.Name.Split("-")[0].Trim()};
        
        var response = await MakeAccountingPutRequest($"TrackingCategories/{trackingId.ToString()}/Options", option);
        
        return JsonConvert.DeserializeObject<XeroResponseTrackingCategoryDto>(response) ?? new XeroResponseTrackingCategoryDto();
    }
    
    private async Task<XeroResponseContactsDto> CreateContact(Member member)
    {
        var contact = new XeroCreateContactDto { Name = member.Club.Name };
        var response = await MakeAccountingPutRequest("Contacts", contact);
        
        return JsonConvert.DeserializeObject<XeroResponseContactsDto>(response) ?? new XeroResponseContactsDto();
    }
}
