using System;
using System.Collections.Generic;
using System.Text;

namespace metiers.shared
{
    public class LoginResult
    {
        public string token { get; set; }
        public string email { get; set; }
        public string id { get; set; }
        public DateTime expiration { get; set; }
    }
}
