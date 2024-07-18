using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Module.ReuqestModule
{
    public class RefreshTokenRequest
    {
        public string refreshToken { get; set; }
        public string accessedToken { get; set;}
    }
}
