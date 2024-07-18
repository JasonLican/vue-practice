using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Module.EntityDto
{
    public class PermissionDataDto
    {
        public string Code { get; set; }
        public string Label { get; set; }
        public List<PermissionDataDto>? Children { get; set; }
    }
}
