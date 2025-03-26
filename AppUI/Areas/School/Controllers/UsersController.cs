using AppUI.Models;
using AppUI.Models.User;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppUI.Areas.School.Controllers
{
    [Authorize]
    [Area("School")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<RoleModel> roles = _mapper.Map<IEnumerable<RoleModel>>(await _roleService.GetAllAsync());
            ViewBag.Roles = new SelectList(roles, "RoleId", "Name");

            UserModel user = id == null ? new UserModel() : _mapper.Map<UserModel>(await _userService.GetByIdAsync((int)id));
            return user != null ? View(user) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<UserListModel> users = _mapper.Map<IEnumerable<UserListModel>>(await _userService.GetAllAsync());
            return Json(users);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UserModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (user.UserId == 0)
                    {
                        await _userService.CreateAsync(_mapper.Map<User>(user));
                        TempData["success"] = "Usuario registrado existosamente.";
                    }
                    else
                    {
                        await _userService.UpdateAsync(_mapper.Map<User>(user));
                        TempData["success"] = "Usuario actualizado existosamente.";
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            IEnumerable<RoleModel> roles = _mapper.Map<IEnumerable<RoleModel>>(await _roleService.GetAllAsync());
            ViewBag.Roles = new SelectList(roles, "RoleId", "Name");

            return View(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DisableAsync(id);
                return Json(new { success = true, message = "Usuario deshabilitado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
