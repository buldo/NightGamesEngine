﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nge.Web.Models.PlayViewModels
{
    public class IndexViewModel
    {
        public string CodeToEnter { get; set; }

        public List<CodeViewModel> Codes;
    }
}
