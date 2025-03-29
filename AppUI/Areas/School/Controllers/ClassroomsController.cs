using AppUI.Models;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    [Authorize]
    [Area("School")]
    public class ClassroomsController : Controller
    {
        private readonly IClassroomService _classroomService;
        private readonly IMapper _mapper;

        public ClassroomsController(IClassroomService classroomService, IMapper mapper)
        {
            _classroomService = classroomService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
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
                        await _classroomService.CreateAsync(_mapper.Map<Classroom>(classroom));
                        TempData["success"] = "Grupo actualizado exitosamente.";
                    }

                    return View ("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

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