using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nge.Web.Models
{
    public class Code
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
