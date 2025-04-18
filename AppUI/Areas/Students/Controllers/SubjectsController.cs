using AppUI.Models;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        public IActionResult Subject()
        {
            return View();
        }

        public async Task<IActionResult> GetSubjectsByStudentId(int studentId)
        {
            IEnumerable<ClassroomSubjectModel> classroomSubjects = _mapper.Map<IEnumerable<ClassroomSubjectModel>>(await _classroomSubjectService.GetAllByStudentIdAsync(studentId));
            return Json(classroomSubjects);
        }
    }
}
