﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.UserControllers
{
    public class VisitorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}