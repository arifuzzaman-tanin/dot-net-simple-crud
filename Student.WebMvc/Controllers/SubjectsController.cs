using Domain.Entites;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.SubjectExt;

namespace Student.WebMvc.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // GET: SubjectsController
        public ActionResult Index()
        {
            return View(_subjectService.GetAll());
        }

        // GET: SubjectsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubjectsController/Create
        public ActionResult Create()
        {
            var languages = from Language language in Enum.GetValues(typeof(Language))
                            select new
                            {
                                LanguageId = (int)language,
                                Name = language.ToString()
                            };
            ViewBag.Languages = new SelectList(languages, "LanguageId", "Name");

            return View();
        }

        // POST: SubjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("SubjectId,Name,Class,SubjectLanguage")] Subject subject)
        {
            try
            {
                var languages = from Language language in Enum.GetValues(typeof(Language))
                                select new
                                {
                                    LanguageId = (int)language,
                                    Name = language.ToString()
                                };
                ViewBag.Languages = new SelectList(languages, "LanguageId", "Name", subject.SubjectLanguage);

                if (ModelState.IsValid)
                {
                    var isDuplicated = _subjectService.CheckDuplicateLang(subject.Name, subject.Class, (int)subject.SubjectLanguage);
                    if (isDuplicated)
                    {
                        ModelState.AddModelError(string.Empty, "Duplicate subject.");
                        return View(subject);
                    }
                    _subjectService.Add(subject);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(subject);
            }

            return View(subject);
        }

        // GET: SubjectsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var subject = _subjectService.Find(Convert.ToInt32(id));

            if (subject == null)
            {
                return NotFound();
            }

            var languages = from Language language in Enum.GetValues(typeof(Language))
                            select new
                            {
                                LanguageId = (int)language,
                                Name = language.ToString()
                            };
            ViewBag.Languages = new SelectList(languages, "LanguageId", "Name", subject.SubjectLanguage);

            return View(subject);
        }

        // POST: SubjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("SubjectId,Name,Class,SubjectLanguage")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            var languages = from Language language in Enum.GetValues(typeof(Language))
                            select new
                            {
                                LanguageId = (int)language,
                                Name = language.ToString()
                            };
            ViewBag.Languages = new SelectList(languages, "LanguageId", "Name", subject.SubjectLanguage);

            if (ModelState.IsValid)
            {
                var isDuplicated = _subjectService.CheckDuplicateLang(subject.Name, subject.Class, (int)subject.SubjectLanguage);
                if (isDuplicated)
                {
                    ModelState.AddModelError(string.Empty, "Duplicate subject.");
                    return View(subject);
                }

                _subjectService.Update(subject);
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }

        // GET: SubjectsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var subject = _subjectService.Find(Convert.ToInt32(id));

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: SubjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
