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
    [Authorize(Roles = "1, 2, 3, 4")]
    [Area("School")]
    public class ClassroomsController : Controller
    {
        private readonly IClassroomService _classroomService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ClassroomsController(IClassroomService classroomService, IUserService userService, IMapper mapper)
        {
            _classroomService = classroomService;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<UserModel> users = _mapper.Map<IEnumerable<UserModel>>(await _userService.GetAllTeachersAsync());
            ViewBag.Teachers = new SelectList(users, "UserId", "Username");

            ClassroomModel classroom = id == null ? new ClassroomModel() : _mapper.Map<ClassroomModel>(await _classroomService.GetByIdAsync((int)id));
            return classroom != null ? View(classroom) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetClassrooms()
        {
            IEnumerable<ClassroomModel> classrooms = _mapper.Map<IEnumerable<ClassroomModel>>(await _classroomService.GetAllAsync());
            return Json(classrooms);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ClassroomModel classroom)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (classroom.ClassroomId == 0)
                    {
                        await _classroomService.CreateAsync(_mapper.Map<Classroom>(classroom));
                        TempData["success"] = "Grupo registrado exitosamente.";
                    }
                    else
                    {
                        await _classroomService.UpdateAsync(_mapper.Map<Classroom>(classroom));
                        TempData["success"] = "Grupo actualizado exitosamente.";
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            IEnumerable<UserModel> users = _mapper.Map<IEnumerable<UserModel>>(await _userService.GetAllTeachersAsync());
            ViewBag.Teachers = new SelectList(users, "UserId", "Username");

            return View(classroom);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _classroomService.DeleteAsync(id);
                return Json(new { success = true, message = "Grupo eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}