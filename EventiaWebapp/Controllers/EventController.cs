﻿using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyEvent()
        {
            return View();

        }

        public IActionResult JoinEvent()
        {
            return View();
        }
    }
}