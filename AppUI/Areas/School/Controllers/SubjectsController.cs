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
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            SubjectModel subject = id == null ? new SubjectModel() : _mapper.Map<SubjectModel>(await _subjectService.GetByIdAsync((int)id));
            return subject != null ? View(subject) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            IEnumerable<SubjectModel> subjects = _mapper.Map<IEnumerable<SubjectModel>>(await _subjectService.GetAllAsync());
            return Json(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(SubjectModel subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (subject.SubjectId == 0)
                    {
                        await _subjectService.CreateAsync(_mapper.Map<Subject>(subject));
                        TempData["success"] = "Materia registrada exitosamente.";
                    }
                    else
                    {
                        await _subjectService.UpdateAsync(_mapper.Map<Subject>(subject));
                        TempData["success"] = "Materia actualizada exitosamente.";
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View(subject);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _subjectService.DeleteAsync(id);
                return Json(new { success = true, message = "Materia deshabilitada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
