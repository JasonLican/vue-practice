using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Module.ReuqestModule
{
    public class UserSearchRequest : BasePageDataRequest
    {
        public string UserName { get; set; }
    }
}
