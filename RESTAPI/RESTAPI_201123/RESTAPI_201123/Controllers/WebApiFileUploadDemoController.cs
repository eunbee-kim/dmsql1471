using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI_201123.Controllers
{
    public class WebApiFileUploadDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
