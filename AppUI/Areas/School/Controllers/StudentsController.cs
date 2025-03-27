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
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            StudentModel student = id == null ? new StudentModel() : _mapper.Map<StudentModel>(await _studentService.GetByIdAsync((int)id));
            return student != null ? View(student) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            IEnumerable<StudentModel> students = _mapper.Map<IEnumerable<StudentModel>>(await _studentService.GetAllAsync());
            return Json(students);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (student.StudentId == 0)
                    {
                        await _studentService.CreateAsync(_mapper.Map<Student>(student), student.EmailAddress);
                        TempData["success"] = "Estudiante registrado exitosamente.";
                    }
                    else
                    {
                        await _studentService.UpdateAsync(_mapper.Map<Student>(student));
                        TempData["success"] = "Estudiante actualizado exitosamente.";
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View(student);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DisableAsync(id);
                return Json(new { success = true, message = "Estudiante deshabilitado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
