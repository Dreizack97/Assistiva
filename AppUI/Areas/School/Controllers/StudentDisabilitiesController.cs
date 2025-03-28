using AppUI.Models.StudentDisability;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    [Authorize]
    [Area("School")]
    public class StudentDisabilitiesController : Controller
    {
        private readonly IStudentDisabilityService _studentDisabilitiesService;
        private readonly IMapper _mapper;

        public StudentDisabilitiesController(IStudentDisabilityService studentDisabilitiesService, IMapper mapper)
        {
            _studentDisabilitiesService = studentDisabilitiesService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            StudentDisabilityModel disability = id == null ? new StudentDisabilityModel() : _mapper.Map<StudentDisabilityModel>(await _studentDisabilitiesService.GetByIdAsync((int)id));
            return disability != null ? View(disability) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetDisabilitiesByStudentId(int studentId)
        {
            IEnumerable<StudentDisabilityListModel> disabilities = _mapper.Map<IEnumerable<StudentDisabilityListModel>>(await _studentDisabilitiesService.GetAllByStudentIdAsync(studentId));
            return Json(disabilities);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(StudentDisabilityModel studentDisability)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (studentDisability.Id == 0)
                    {
                        await _studentDisabilitiesService.CreateAsync(_mapper.Map<StudentDisability>(studentDisability));
                        TempData["success"] = "Discapacidad registrada exitosamente.";
                    }
                    else
                    {
                        await _studentDisabilitiesService.UpdateAsync(_mapper.Map<StudentDisability>(studentDisability));
                        TempData["success"] = "Discapacidad actualizada exitosamente.";
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View(studentDisability);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentDisabilitiesService.DeleteAsync(id);
                return Json(new { success = true, message = "Discapacidad eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}