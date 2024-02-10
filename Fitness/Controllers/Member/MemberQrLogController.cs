using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Fitness.Dtos.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fitness.Controllers;

public class MemberQrLogController : CommonController<MemberQrLogController>
{
    private readonly IMapper mapper;
    private readonly IMemberQrLogService memberQrLogService;
    private readonly IConfiguration _configuration;

    public MemberQrLogController(IMapper mapper, IMemberQrLogService memberQrLogService, IConfiguration configuration)
    {
        this.mapper = mapper;
        this.memberQrLogService = memberQrLogService;
        _configuration = configuration; 
    }

    [HttpPost("GenerateQr")]    
    public async Task<object> Add([FromBody] MemberQrLogPostDto model)
    {
        var item = mapper.Map<MemberQrLog>(model);
        item.UniqueIdentifier = Guid.NewGuid();
               
        await memberQrLogService.AddAsync(item);

        string qrString = $"{item.UniqueIdentifier}^{model.MemberId}^{model.ClubId}";
        string base64QrString = Convert.ToBase64String(Encoding.UTF8.GetBytes(qrString));
        int ExpirationTimeInSec = Convert.ToInt32(_configuration["QrExpirationInSec"]);

        return new {
            base64QrString,
            ExpirationTimeInSec
        };
    }



    [AllowAnonymous]
    [HttpPost("ValidateUser")]
    public async Task<bool> ValidateUserAsync([FromBody] MemberQrValidityPostDto model, [FromQuery] string club = "")
    {
        bool isValid;
        if (model.Data.IsNullOrEmpty() || !IsBase64String(model.Data!))
        {
            return false;
        }

        byte[] byteArray = Convert.FromBase64String(model.Data!);

        string decodedString = Encoding.UTF8.GetString(byteArray);
        string[] dataParts = decodedString.Split('^');
        string uniqueIdentifier = dataParts[0];
        string memberIdStr = dataParts[1];
        string clubIdStr = dataParts[2];

        if (!club.IsNullOrEmpty())
        {
            bool clubAreEqual = club.Equals(clubIdStr);

            if (clubAreEqual)
            {
                if (Guid.TryParse(uniqueIdentifier, out Guid parsedGuid))
                {
                    isValid = await memberQrLogService.GetQrValidityAsync(parsedGuid, int.Parse(clubIdStr), Convert.ToInt64(_configuration["QrExpirationInSec"]));
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

        }
        else
        {
            isValid = false;
        }

        return isValid;
    }

    private static bool IsBase64String(string s)
    {
        s = s.Trim();
        return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }



}
