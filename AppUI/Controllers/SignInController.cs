using AppUI.Models.User;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppUI.Controllers
{
    public class SignInController : Controller
    {
        private readonly IUserService _userService;

        public SignInController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            return claimsUser.Identity?.IsAuthenticated == true ? RedirectToAction("Index", "Home") : View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string recoveryCode)
        {
            if (string.IsNullOrWhiteSpace(recoveryCode))
                return View("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User? _user = await _userService.SignInAsync(user.Username, user.Password);

                    if (user != null)
                    {
                        List<Claim> claims = new List<Claim>()
                        {
                            new(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                            new(ClaimTypes.Name, _user.Username),
                            new(ClaimTypes.Role, _user.RoleId.ToString()), // TODO: Posible uso de DTO para roles
                            new(ClaimTypes.Email, _user.Email),
                            new(ClaimTypes.Uri, _user.UrlPicture ?? "")
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                    return View("Index", user);
                }
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.SetRecoveryCodeAsync(user.Email);
                    TempData["success"] = "Se ha enviado un correo electrónico con instrucciones para restablecer tu contraseña.";
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (user.NewPassword.Equals(user.ConfirmPassword))
                        throw new TaskCanceledException("Las contraseñas no coinciden.");

                    await _userService.IsValidRecoveryCodeAsync(user.RecoveryCode, user.NewPassword);
                    TempData["success"] = "Tu contraseña ha sido restablecida correctamente.";
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View(user);
        }
    }
}