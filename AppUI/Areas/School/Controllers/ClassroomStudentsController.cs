using System.Threading.Tasks;
using AppUI.Models;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    public class ClassroomStudentsController : Controller
    {
        private readonly IClassroomStudentService _classroomStudentService;
        private readonly IClassroomService _classroomService;
        private readonly IMapper _mapper;

        public ClassroomStudentsController(IClassroomStudentService classroomStudentService, IClassroomService classroomService, IMapper mapper)
        {
            _classroomStudentService = classroomStudentService;
            _classroomService = classroomService;
            _mapper = mapper;
        }

        [Route("/School/Classrooms/{classroomId}/Students/")]
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
    }
}