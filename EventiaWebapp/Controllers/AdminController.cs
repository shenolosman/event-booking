﻿using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly Database _databaseService;
        private readonly SignInManager<User> _signInManager;

        public AdminController(UserManager<User> userManager, Database databaseService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _databaseService = databaseService;
            _signInManager = signInManager;
        }
        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public async Task<ActionResult> EditUser(string id)
        {
            var OrganizerUser = await _userManager.FindByIdAsync(id);

            return View(OrganizerUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(User user)
        {
            var organizerUser = await _userManager.FindByIdAsync(user.Id);

            if (organizerUser == null) return NotFound();
            //bool userTrue = user.isOrganizer;
            if (user.isOrganizer == true)
            {
                // userTrue =
                organizerUser.isOrganizer = true;
                await _userManager.AddToRoleAsync(organizerUser, "Organizer");
                await _userManager.UpdateAsync(organizerUser);
                //  await _eventHandler.ChangeStatusofUser(userTrue, OrganizerUser);
                return RedirectToAction(nameof(Index));
            }
            else if (user.isOrganizer == false)
            {
                // userTrue = 
                organizerUser.isOrganizer = false;
                await _userManager.AddToRoleAsync(organizerUser, "Attendee");
                await _userManager.RemoveFromRoleAsync(organizerUser, "Organizer");
                await _userManager.UpdateAsync(organizerUser);
                // await _eventHandler.ChangeStatusofUser(userTrue, OrganizerUser);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public ViewResult DbReset()
        {
            return View();
        }
        [HttpPost, ActionName("DbReset")]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> DbResetConfirmation()
        {
            await _databaseService.RecreateAndSeed();
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
