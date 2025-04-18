using AppUI.Models;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppUI.Areas.Students.Controllers
{
    [Authorize]
    [Area("Students")]
    public class SubjectsController : Controller
    {
        private readonly IClassroomSubjectService _classroomSubjectService;
        private readonly IMapper _mapper;

        public SubjectsController(IClassroomSubjectService classroomSubjectService, IMapper mapper)
        {
            _classroomSubjectService = classroomSubjectService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            // FIX: Implementar solución para obtener el Id del estudiante.
            ViewBag.StudentId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).Single());
            return View();
        }

        public IActionResult Subject()
        {
            return View();
        }

        public async Task<IActionResult> GetSubjectsByStudentId(int studentId)
        {
            // FIX: Utilizar el Id del estudiante desde el contexto de la sesión.
            IEnumerable<ClassroomSubjectModel> classroomSubjects = _mapper.Map<IEnumerable<ClassroomSubjectModel>>(await _classroomSubjectService.GetAllByStudentIdAsync(1));
            return Json(classroomSubjects);
        }
    }
}
