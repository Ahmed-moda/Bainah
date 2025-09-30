using Bainah.Core.Entities;
using Core.DTOs.Otp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOtpService
    {
        Task<OtpExpDto> GenerateOtpAsync(User user);
        Task<bool> ValidateOtpAsync(User user, string otp);
    }
}
