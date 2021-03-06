﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nge.Web.Models.PlayViewModels
{
    public class IndexViewModel
    {
        public string CodeToEnter { get; set; } = string.Empty;

        public List<ShortCodeViewModel> Codes { get; set; }

        public List<DetailCodeViewModel> EnteredCodes { get; set; }
    }
}
