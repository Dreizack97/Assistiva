using AppUI.Models;
using AppUI.Models.StudentDisability;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppUI.Areas.School.Controllers
{
    [Authorize(Roles = "1, 2, 4")]
    [Area("School")]
    public class StudentDisabilitiesController : Controller
    {
        private readonly IStudentDisabilityService _studentDisabilitiesService;
        private readonly IDisabilityService _disabilityService;
        private readonly IMapper _mapper;

        public StudentDisabilitiesController(IStudentDisabilityService studentDisabilitiesService, IDisabilityService disabilityService, IMapper mapper)
        {
            _studentDisabilitiesService = studentDisabilitiesService;
            _disabilityService = disabilityService;
            _mapper = mapper;
        }

        [Route("/School/Students/Upsert/{studentId}/Disabilities/")]
        public IActionResult Index(int studentId)
        {
            ViewBag.StudentId = studentId;
            return View();
        }

        [Route("/School/Students/Upsert/{studentId}/Disabilities/Upsert/{id?}")]
        public async Task<IActionResult> Upsert(int studentId, int? id)
        {
            ViewBag.StudentId = studentId;

            IEnumerable<DisabilityModel> disabilities = _mapper.Map<IEnumerable<DisabilityModel>>(await _disabilityService.GetAllAsync());
            ViewBag.Disabilities = new SelectList(disabilities, "DisabilityId", "Name");

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
        [Route("/School/Students/Upsert/{studentId}/Disabilities/Upsert/{id?}")]
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

                    return Redirect($"/School/Students/Upsert/{studentDisability.StudentId}/Disabilities");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            ViewBag.StudentId = studentDisability.StudentId;

            IEnumerable<DisabilityModel> disabilities = _mapper.Map<IEnumerable<DisabilityModel>>(await _disabilityService.GetAllAsync());
            ViewBag.Disabilities = new SelectList(disabilities, "DisabilityId", "Name");

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