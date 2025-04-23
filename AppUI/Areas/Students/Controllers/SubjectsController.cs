using AppUI.Models;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppUI.Areas.Students.Controllers
{
    [Authorize]
    [Area("Students")]
    public class SubjectsController : Controller
    {
        private readonly IClassroomSubjectService _classroomSubjectService;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectsController(IClassroomSubjectService classroomSubjectService, ISubjectService subjectService, IMapper mapper)
        {
            _classroomSubjectService = classroomSubjectService;
            _subjectService = subjectService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ViewBag.StudentId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "StudentId").Select(c => c.Value).Single());
            return View();
        }

        public async Task<IActionResult> Subject(int id)
        {
            SubjectModel subjectModel = _mapper.Map<SubjectModel>(await _subjectService.GetByIdAsync(id));
            return subjectModel != null ? View(subjectModel) : NotFound();
        }

        public async Task<IActionResult> GetSubjectsByStudentId(int studentId)
        {
            IEnumerable<ClassroomSubjectModel> classroomSubjects = _mapper.Map<IEnumerable<ClassroomSubjectModel>>(await _classroomSubjectService.GetAllByStudentIdAsync(studentId));
            return Json(classroomSubjects);
        }
    }
}
