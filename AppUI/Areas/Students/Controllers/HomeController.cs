using AppUI.Models.User;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppUI.Areas.Students.Controllers
{
    [Authorize]
    [Area("Students")]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public HomeController(IWebHostEnvironment webHostEnvironment, IUserService userService, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).Single());
            UserProfileModel userProfile = _mapper.Map<UserProfileModel>(await _userService.GetProfileByIdAsync(userId));

            ViewBag.IsBirthday = false;

            if (userProfile.Student != null)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                if (userProfile.Student.DateOfBirth.Month == today.Month && userProfile.Student.DateOfBirth.Day == today.Day)
                {
                    ViewBag.IsBirthday = true;
                    ViewBag.FullName = userProfile.Student.FullName;
                }
            }

            return View();
        }

        public async Task<IActionResult> Profile()
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).Single());
            UserProfileModel userProfile = _mapper.Map<UserProfileModel>(await _userService.GetProfileByIdAsync(userId));

            return View(userProfile);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(UserProfileModel userProfile, IFormFile? picture)
        {
            const int MAX_PICTURE_SIZE = 5242880; //5MB in bytes

            if (picture != null && picture.Length <= MAX_PICTURE_SIZE)
            {
                try
                {
                    string pictureName = Guid.NewGuid().ToString("N") + Path.GetExtension(picture.FileName);
                    string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "img", "users", pictureName);

                    if (!string.IsNullOrWhiteSpace(userProfile.UrlPicture))
                    {
                        string oldPicturePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", userProfile.UrlPicture.TrimStart('\\'));

                        if (System.IO.File.Exists(oldPicturePath))
                            System.IO.File.Delete(oldPicturePath);
                    }

                    using (Stream stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                    }

                    userProfile.UrlPicture = uploadPath.Substring(uploadPath.IndexOf(@"\img"));

                    if (await _userService.UpdatePictureAsync(userProfile.UserId, userProfile.UrlPicture))
                        TempData["success"] = "Imagen de perfil actualizada exitosamente.";
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }
            else
                TempData["info"] = "No se ha seleccionado una imagen o supera el tamaño máximo permitido.";

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserProfileModel userProfile)
        {
            if (userProfile.ChangePassword != null)
            {
                if (!userProfile.ChangePassword.NewPassword.Equals(userProfile.ChangePassword.ConfirmPassword))
                {
                    TempData["info"] = "Las contraseñas no coinciden.";
                    return RedirectToAction("Profile");
                }
            }

            try
            {
                await _userService.IsValidPasswordAsync(userProfile.UserId, userProfile.ChangePassword!.ActualPassword, userProfile.ChangePassword!.NewPassword);
                TempData["success"] = "Contraseña actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["info"] = ex.Message;
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateData(UserProfileModel userProfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateAsync(_mapper.Map<User>(userProfile));
                    TempData["success"] = "Información actualizada exitosamente.";

                    return RedirectToAction("Profile");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View("Profile", userProfile);
        }
    }
}
