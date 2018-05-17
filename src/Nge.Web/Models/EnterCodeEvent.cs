using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nge.Web.Models
{
    public class EnterCodeEvent
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public string Value { get; set; }

        public DateTimeOffset Entered { get; set; }
    }
}
