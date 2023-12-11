using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalPayment.Integration.Api.V1.Models.Response
{
    public class AuthenticationTokenResponse
    {
        public AuthenticationTokenResponse(string token)
        {
            Token = token;
        }

        public string Token { get; private set ; } = default!;
    }
}
