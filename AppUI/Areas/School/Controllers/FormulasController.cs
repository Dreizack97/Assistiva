using AppUI.Models.Formula;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    [Authorize]
    [Area("School")]
    public class FormulasController : Controller
    {
        private readonly IFormulaService _formulaService;
        private readonly IMapper _mapper;

        public FormulasController(IFormulaService formulaService, IMapper mapper)
        {
            _formulaService = formulaService;
            _mapper = mapper;
        }

        [Route("/School/Subjects/{subjectId}/Formulas")]
        [Authorize(Roles = "1, 2, 3")]
        public IActionResult Index(int subjectId)
        {
            ViewBag.SubjectId = subjectId;

            return View();
        }

        [Route("/School/Subjects/{subjectId}/Formulas/Upsert/{formulaId?}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> Upsert(int subjectId, int? formulaId)
        {
            ViewBag.SubjectId = subjectId;

            FormulaModel formula = formulaId == null ? new FormulaModel() : _mapper.Map<FormulaModel>(await _formulaService.GetByIdAsync((int)formulaId));
            return formula != null ? View(formula) : NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "1, 2, 3, 5")]
        public async Task<IActionResult> GetFormulasBySubjectId(int subjectId)
        {
            IEnumerable<FormulaListModel> formulas = _mapper.Map<IEnumerable<FormulaListModel>>(await _formulaService.GetAllBySubjectIdAsync(subjectId));
            return Json(formulas);
        }

        [HttpPost]
        [Route("/School/Subjects/{subjectId}/Formulas/Upsert/{formulaId?}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> Upsert(FormulaModel formula)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (formula.FormulaId == 0)
                    {
                        await _formulaService.CreateAsync(_mapper.Map<Formula>(formula));
                        TempData["success"] = "Formula registrada exitosamente.";
                    }
                    else
                    {
                        await _formulaService.UpdateAsync(_mapper.Map<Formula>(formula));
                        TempData["success"] = "Formula actualizada exitosamente.";
                    }

                    return Redirect($"/School/Subjects/{formula.SubjectId}/Formulas");
                }
                catch (Exception ex)
                {
                    TempData["info"] = ex.Message;
                }
            }

            ViewBag.SubjectId = formula.SubjectId;
            return View(formula);
        }

        [HttpDelete]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _formulaService.DeleteAsync(id);
                return Json(new { success = true, message = "Formula eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
