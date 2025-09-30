using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Otp
{
    public class OtpExpDto
    {
        public string Otp { get; set; }
        public DateTime Expiry { get; set; }
    }
}
