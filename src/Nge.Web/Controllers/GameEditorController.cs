using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nge.Web.Controllers
{
    public class GameEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}