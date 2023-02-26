using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.DTOs.UserDTOs
{
    public class RegisterUserResponseDto
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
