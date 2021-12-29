using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Dto.Authentication
{
    public class TokenRequest
    {
        //[JsonProperty("username")]
        public string UserName { get; set; }

        //[JsonProperty("password")]
        public string Password { get; set; }
    }
}
