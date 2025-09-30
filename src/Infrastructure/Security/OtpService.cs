using Bainah.Core.Entities;
using Core.DTOs.Otp;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class OtpService : IOtpService
    {
        private readonly UserManager<User> _userManager;
        private readonly Random _random = new();

        public OtpService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OtpExpDto> GenerateOtpAsync(User user)
        {
            var result = new OtpExpDto();
            if (user.OtpFailedAttempts >= 3)
            {
                user.OtpFailedAttempts = 0;
                user.LockoutEnd = null;


            }
            string otp = _random.Next(100000, 999999).ToString();
            user.OtpCode = otp;
            user.OtpExpiry = DateTime.Now.AddMinutes(5);
            user.OtpFailedAttempts++;

            if (user.OtpFailedAttempts >= 3)
                user.LockoutEnd = DateTime.Now.AddHours(1); // lock for 1 hour

            await _userManager.UpdateAsync(user);
            result.Otp = otp;
            result.Expiry = user.OtpExpiry.Value;
            return result;
        }

        public async Task<bool> ValidateOtpAsync(User user, string otp)
        {
            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
                return false;

            if (user.OtpExpiry < DateTime.UtcNow)
                return false;

            if (user.OtpCode == otp)
            {
                user.OtpCode = null;
                user.OtpExpiry = null;
                user.OtpFailedAttempts = 0;
                user.LockoutEnd = null;
                await _userManager.UpdateAsync(user);
                return true;
            }            

            return false;
        }
    }
}
