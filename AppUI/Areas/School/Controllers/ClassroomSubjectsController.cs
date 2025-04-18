using AppUI.Models;
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
    public class ClassroomSubjectsController : Controller
    {
        private readonly IClassroomSubjectService _classroomSubjectService;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public ClassroomSubjectsController(IClassroomSubjectService classroomSubjectService, ISubjectService subjectService, IMapper mapper)
        {
            _classroomSubjectService = classroomSubjectService;
            _subjectService = subjectService;
            _mapper = mapper;
        }

        [Route("/School/Classrooms/{classroomId}/Subjects")]
        public IActionResult Index(int classroomId)
        {
            ViewBag.ClassroomId = classroomId;
            return View();
        }

        [Route("/School/Classrooms/{classroomId}/Subjects/Upsert/{id?}")]
        public async Task<IActionResult> Upsert(int classroomId, int? id)
        {
            ViewBag.ClassroomId = classroomId;

            IEnumerable<SubjectModel> subjects = _mapper.Map<IEnumerable<SubjectModel>>(await _subjectService.GetAllAsync());
            ViewBag.Subjects = new SelectList(subjects, "SubjectId", "Name");

            ClassroomSubjectModel classroomSubject = id == null ? new ClassroomSubjectModel() : _mapper.Map<ClassroomSubjectModel>(await _classroomSubjectService.GetByIdAsync((int)id));
            return classroomSubject != null ? View(classroomSubject) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjectsByClassroomId(int classroomId)
        {
            IEnumerable<ClassroomSubjectModel> classroomSubjects = _mapper.Map<IEnumerable<ClassroomSubjectModel>>(await _classroomSubjectService.GetAllByClassroomIdAsync(classroomId));
            return Json(classroomSubjects);
        }

        [HttpPost]
        [Route("/School/Classrooms/{classroomId}/Subjects/Upsert/{id?}")]
        public async Task<IActionResult> Upsert(ClassroomSubjectModel classroomSubject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (classroomSubject.Id == 0)
                    {
                        await _classroomSubjectService.CreateAsync(_mapper.Map<ClassroomSubject>(classroomSubject));
                        TempData["success"] = "Materia asignada exitosamente.";
                    }
                    else
                    {
                        await _classroomSubjectService.UpdateAsync(_mapper.Map<ClassroomSubject>(classroomSubject));
                        TempData["success"] = "Materia actualizada exitosamente.";
                    }

                    return Redirect($"/School/Classrooms/{classroomSubject.ClassroomId}/Subjects");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            ViewBag.ClassroomId = classroomSubject.ClassroomId;

            IEnumerable<SubjectModel> subjects = _mapper.Map<IEnumerable<SubjectModel>>(await _subjectService.GetAllAsync());
            ViewBag.Subjects = new SelectList(subjects, "SubjectId", "Name");

            return View(classroomSubject);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _classroomSubjectService.DeleteAsync(id);
                return Json(new { success = true, message = "Materia eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
