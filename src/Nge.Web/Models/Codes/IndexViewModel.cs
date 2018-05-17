using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nge.Web.Models.Codes
{
    public class IndexViewModel
    {
        public string NewCodeType { get; set; } = string.Empty;
        public string NewCodeValue { get; set; } = string.Empty;

        public IEnumerable<Code> Codes { get; set; }
    }
}
