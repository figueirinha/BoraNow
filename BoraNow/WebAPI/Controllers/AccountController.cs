﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataLayer.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers.Web.UserControllers;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Support;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> UserManager { get; set; }
        private SignInManager<User> SignInManager { get; set; }
        private RoleManager<Role> RoleManager { get; set; }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index), "Home");
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index), "Home");
        }

        public AccountController(UserManager<User> uManager, SignInManager<User> sManager, RoleManager<Role> rManager)
        {
            UserManager = uManager;
            SignInManager = sManager;
            RoleManager = rManager;
        }

        [AllowAnonymous]
        [HttpPost("/GenerateToken")]
        public IActionResult GenerateToken(LoginViewModel vm)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
           
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
           
            var accountBo = new AccountBusinessController(UserManager, RoleManager);
            var person = new Profile(vm.Description, vm.PhotoPath);
            var registerOperation = await accountBo.Register(vm.UserName, vm.Email, vm.Password, person, vm.Role);
            if (registerOperation.Success)
            {
                if (vm.Role == "Visitor")
                {
                    return RedirectToAction("Create", "Visitors");
                }
                if(vm.Role == "Company")
                {
                    return RedirectToAction("Create", "Companies");
                }
            }
                //return OperationSuccess("The account was successfuly registered!");
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, registerOperation.Message);

            return View(vm);
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var loginOperation = await SignInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (loginOperation.Succeeded) return OperationSuccess("Welcome User");
            else
            {
                TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, loginOperation.ToString());
                return View(vm);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}