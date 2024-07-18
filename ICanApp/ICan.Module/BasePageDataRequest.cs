using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Module
{
    public class BasePageDataRequest
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; } = 0;
    }
    public class BasePageDataResponse : BaseResponse
    {
        public int Total { get; set; }
    }
}
