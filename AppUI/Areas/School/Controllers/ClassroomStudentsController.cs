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
    public class ClassroomStudentsController : Controller
    {
        private readonly IClassroomStudentService _classroomStudentService;
        private readonly IClassroomService _classroomService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public ClassroomStudentsController(IClassroomStudentService classroomStudentService, IClassroomService classroomService, IStudentService studentService, IMapper mapper)
        {
            _classroomStudentService = classroomStudentService;
            _classroomService = classroomService;
            _studentService = studentService;
            _mapper = mapper;
        }

        [Route("/School/Classrooms/{classroomId}/Students")]
        public IActionResult Index(int classroomId)
        {
            ViewBag.ClassroomId = classroomId;
            return View();
        }

        [Route("/School/Classrooms/{classroomId}/Students/Upsert/{id?}")]
        public async Task<IActionResult> Upsert(int classroomId, int? id)
        {
            ViewBag.ClassroomId = classroomId;

            ClassroomStudentModel student = id == null ? new ClassroomStudentModel() : _mapper.Map<ClassroomStudentModel>(await _classroomStudentService.GetByIdAsync((int)id));
            return student != null ? View(student) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsByClassroomId(int classroomId)
        {
            IEnumerable<ClassroomStudentModel> students = _mapper.Map<IEnumerable<ClassroomStudentModel>>(await _classroomStudentService.GetAllByClassroomIdAsync(classroomId));
            return Json(students);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentByName(string studentName)
        {
            StudentModel student = _mapper.Map<StudentModel>(await _studentService.GetByNameAsync(studentName));
            return Json(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            StudentModel student = _mapper.Map<StudentModel>(await _studentService.GetByIdAsync(studentId));
            return Json(student);
        }

        [HttpPost]
        [Route("/School/Classrooms/{classroomId}/Students/Upsert/{id?}")]
        public async Task<IActionResult> Upsert(ClassroomStudentModel student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (student.Id == 0)
                    {
                        await _classroomStudentService.CreateAsync(_mapper.Map<ClassroomStudent>(student));
                        TempData["success"] = "Estudiante asignado exitosamente.";
                    }
                    else
                    {
                        await _classroomStudentService.UpdateAsync(_mapper.Map<ClassroomStudent>(student));
                        TempData["success"] = "Estudiante actualizado exitosamente.";
                    }

                    return Redirect($"/School/Classrooms/{student.ClassroomId}/Students");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            ViewBag.ClassroomId = student.ClassroomId;
            return View(student);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _classroomStudentService.DeleteAsync(id);
                return Json(new { success = true, message = "Estudiante eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}