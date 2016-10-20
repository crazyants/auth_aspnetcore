﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Auth.Jwt
{
    public class TokenLogoutRequest
    {
        public string SpName { get; set; }
        public string IdpUrl { get; set; }
        public string Username { get; set; }
        public string RelayState { get; set; }
    }
}
