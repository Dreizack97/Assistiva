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
    public class DisabilitiesController : Controller
    {
        private readonly IDisabilityService _disabilityService;
        private readonly IMapper _mapper;

        public DisabilitiesController(IDisabilityService disabilityService, IMapper mapper)
        {
            _disabilityService = disabilityService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            DisabilityModel disability = id == null ? new DisabilityModel() : _mapper.Map<DisabilityModel>(await _disabilityService.GetByIdAsync((int)id));
            return disability != null ? View(disability) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetDisabilities()
        {
            IEnumerable<DisabilityModel> disabilities = _mapper.Map<IEnumerable<DisabilityModel>>(await _disabilityService.GetAllAsync());
            return Json(disabilities);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(DisabilityModel disability)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (disability.DisabilityId == 0)
                    {
                        await _disabilityService.CreateAsync(_mapper.Map<Disability>(disability));
                        TempData["success"] = "Discapacidad registrada exitosamente.";
                    }
                    else
                    {
                        await _disabilityService.UpdateAsync(_mapper.Map<Disability>(disability));
                        TempData["success"] = "Discapacidad actualizada exitosamente.";
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            return View(disability);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _disabilityService.DisableAsync(id);
                return Json(new { success = true, message = "Discapacidad deshabilitada correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
