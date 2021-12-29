using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Dto.Authentication
{
    //[JsonObject("token")]
    public class Token
    {
        //[JsonProperty("secret")]
        public string Secret { get; set; }

        //[JsonProperty("issuer")]
        public string Issuer { get; set; }

        //[JsonProperty("audience")]
        public string Audience { get; set; }

        //[JsonProperty("expiry")]
        public int Expiry { get; set; }

        //[JsonProperty("refreshExpiry")]
        public int RefreshExpiry { get; set; }
    }
}
