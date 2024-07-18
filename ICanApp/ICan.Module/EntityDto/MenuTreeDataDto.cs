using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Module.EntityDto
{
    public class MenuTreeDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string label { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public string? Component { get; set; }
        public Meta Meta { get; set; }
        public List<MenuTreeDataDto> Children { get; set; }

    }
    public class Meta
    {
        public string Title { get; set; }
        public string? ParentTitle { get; set; }
    }
}
