using System.Security.Claims;
using AppUI.Models.User;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    [Authorize]
    [Area("School")]
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).Single());
            UserProfileModel userProfile = _mapper.Map<UserProfileModel>(await _userService.GetProfileByIdAsync(userId));

            return View(userProfile);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(UserProfileModel userProfile, IFormFile? picture)
        {
            const int MAX_PICTURE_SIZE = 5242880; //5MB in bytes

            if (picture != null && picture.Length <= MAX_PICTURE_SIZE)
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

                if (await _userService.UpdatePictureAsync(userProfile.UserId, uploadPath))
                    TempData["success"] = "Imágen de perfil actualizada exitosamente.";
            }
            else
                TempData["info"] = "No se ha seleccionado una imagen o supera el tamaño máximo permitido.";

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
