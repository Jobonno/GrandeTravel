using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.Services;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<MyUser> _userManager;
        private SignInManager<MyUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        

        public AccountController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

       

       [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel vm)
        {
            
            if (ModelState.IsValid)
            {
                MyUser tempUser = new MyUser
                {
                    UserName = vm.Username,
                    Email = vm.Email

                };
                var result = await _userManager.CreateAsync(tempUser, vm.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(tempUser);
                    
                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                        new { userId = tempUser.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    var message = new MimeMessage();
                    message.To.Add(new MailboxAddress(tempUser.UserName, vm.Email));
                    message.From.Add(new MailboxAddress("Grande Travel", "grandetravelproject@gmail.com"));
                    message.Subject = "Confirm Your Account";
                    message.Body = new TextPart("plain")
                    {
                        Text = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>"
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate("grandetravelproject@gmail.com", "Diplomaproject");
                        client.Send(message);
                        client.Disconnect(true);
                    }


                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
               // $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";

                    // Comment out following line to prevent a new user automatically logged on.
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation(3, "User created a new account with password.");
                   


                    //remember to add roles first!!
                    await _userManager.AddToRoleAsync(tempUser, "Customer");
                    //await _signInManager.SignInAsync(tempUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterTravelProvider()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterTravelProvider(RegisterUserViewModel vm)
        {

            if (ModelState.IsValid)
            {
                MyUser tempUser = new MyUser
                {
                    UserName = vm.Username,
                    Email = vm.Email

                };
                var result = await _userManager.CreateAsync(tempUser, vm.Password);
                if (result.Succeeded)
                {
                    //remember to add roles first!!
                    await _userManager.AddToRoleAsync(tempUser, "TravelProvider");
                    await _signInManager.SignInAsync(tempUser,false);
                    return RedirectToAction("UpdateProviderProfile", "Profile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }



        [HttpGet]
        public IActionResult LogIn(string returnUrl = "")
        {
            LoginViewModel vm = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(vm.Username);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty,
                                      "You must have a confirmed email to log in.");
                        return View(vm);
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Username or Password Incorrect");
            return View(vm);
        }

        [HttpPost]        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(AddRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //check if role exists
                var theRole = await _roleManager.FindByNameAsync(vm.NewRole);
                if (theRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(vm.NewRole));
                    //change later to redirect to display roles
                    return RedirectToAction("Index", "TravelPackage");
                }
                else
                {
                    ModelState.AddModelError("", "Role Name already used");
                }
            }
            return View(vm);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

    }
}
