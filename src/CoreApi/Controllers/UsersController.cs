using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using Bainah.CoreApi.Common;
using Bainah.CoreApi.DTOs;
using Core.Interfaces;
using CoreApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Bainah.CoreApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtProvider _jwt;
    private readonly IOtpService _otpService;
    public UsersController(UserManager<User> userManager, IJwtProvider jwt, IOtpService otpService) 
    { _userManager = userManager;
        _jwt = jwt;
        _otpService = otpService;

    }


    [HttpPost("register")]
    public async Task<DataResponse<RegisterRequestDto>> Register([FromBody] RegisterRequestDto dto)
    {
        DataResponse<RegisterRequestDto> result =new DataResponse<RegisterRequestDto>() { Success=true,Message="Done"};
        // NationalId uniqueness check
        if (await _userManager.Users.AnyAsync(u => u.NationalId == dto.NationalId))
        {
            result.Success = false;
            result.Message = "National ID already registered.";
            result.Data=new RegisterRequestDto();
            return result;
        }
            

        // Phone uniqueness check
        if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.PhoneNumber))
        {
            result.Success = false;
            result.Message = "Phone number already registered.";
            result.Data = new RegisterRequestDto();
            return result;
        }

        var user = new User
        {
            UserName = dto.NationalId, // using NationalId as username surrogate
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            NationalId = dto.NationalId,
            Region = dto.Region,
            City = dto.City,
            Street = dto.Street,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        var res = await _userManager.CreateAsync(user);
        if (!res.Succeeded) 
        {
            result.Success = false;
            result.Message = res.Errors.ToString();
            result.Data = new RegisterRequestDto();
            return result;
        }

        await _userManager.AddToRoleAsync(user, "DefaultUser");

           

        return result;
    }



    [HttpPost("request-otp")]

    public async Task<DataResponse<string>> RequestOtp([FromBody] string nationalId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NationalId == nationalId);
        if (user == null)
            return new DataResponse<string>() { Success = false, Message = "User not found.", Data = null };
        try
        {
            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
                return new DataResponse<string>() { Success = false, Message = "This Account locked for one hour", Data = null };


            var otp =await _otpService.GenerateOtpAsync(user);
            return new DataResponse<string>() { Success = true, Message = "OTP sent to registered phone number.", Data = otp };
        }
        catch (Exception ex)
        {
            return new DataResponse<string>() { Success = false, Message = ex.Message, Data = null };
        }
    }

    [HttpPost("verify-otp")]
    public async Task<DataResponse<string>> VerifyOtp([FromBody] OtpVerifyDto dto)
    {
        var result = new DataResponse<string>();
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NationalId == dto.NationalId);
        if (user == null) { 
            result.Success = false;
            result.Message = "User not found.";
            result.Data = null;
            return result;
        }

        var isValid = await _otpService.ValidateOtpAsync(user, dto.OtpCode);
        if (!isValid) { 
            result.Success = false;
            result.Message = "Invalid or expired OTP.";
            result.Data = null;
            return result;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwt.GenerateToken(user, roles);

        return new DataResponse<string>() { Success=true,Message="Done",Data=token};
    }

}
