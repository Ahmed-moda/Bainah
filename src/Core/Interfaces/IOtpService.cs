using Bainah.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(User user);
        Task<bool> ValidateOtpAsync(User user, string otp);
    }
}
