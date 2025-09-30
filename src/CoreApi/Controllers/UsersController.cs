using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using Bainah.CoreApi.Common;
using Bainah.CoreApi.DTOs;
using Core.DTOs.Otp;
using Core.Interfaces;
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
            RegionId = dto.Region,
            CityId = dto.City,
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
    public async Task<DataResponse<OtpExpDto>> RequestOtp([FromBody] LoginRequestDto model)
    {
        var response = new DataResponse<OtpExpDto>();

        // 1. Find user by phone
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found.";
            return response;
        }

        // 2. Check if account is locked
        if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
        {
            response.Success = false;
            response.Message = $"This account is locked until {user.LockoutEnd.Value}.";
            return response;
        }

        // 3. Validate password
        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
            response.Success = false;
            response.Message = "Invalid credentials.";
            return response;
        }

        try
        {
            // 4. Generate OTP if credentials are correct
            var otp = await _otpService.GenerateOtpAsync(user);

            response.Success = true;
            response.Message = "OTP sent to registered phone number.";
            response.Data = otp;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error: {ex.Message}";
            return response;
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
