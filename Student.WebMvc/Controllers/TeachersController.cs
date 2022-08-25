using Domain.Entites;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.TeacherExt;

namespace Student.WebMvc.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: TeachersController
        public ActionResult Index()
        {
            return View(_teacherService.GetAll());
        }

        // GET: TeachersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeachersController/Create
        public ActionResult Create()
        {
            var genders = from Gender sex in Enum.GetValues(typeof(Gender))
                          select new
                          {
                              GenderId = (int)sex,
                              Name = sex.ToString()
                          };
            ViewBag.Genders = new SelectList(genders, "GenderId", "Name");

            return View();
        }

        // POST: TeachersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("TeacherId,Name,Age,Sex,Image,ImageFile")] Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _teacherService.Add(teacher);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(teacher);
            }

            return View(teacher);
        }

        // GET: TeachersController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var teacher = _teacherService.Find(Convert.ToInt32(id));

            if (teacher == null)
            {
                return NotFound();
            }

            var genders = from Gender sex in Enum.GetValues(typeof(Gender))
                          select new
                          {
                              GenderId = (int)sex,
                              Name = sex.ToString()
                          };
            ViewBag.Genders = new SelectList(genders, "GenderId", "Name", teacher.Sex);

            return View(teacher);
        }

        // POST: TeachersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("TeacherId,Name,Age,Sex,Image,ImageFile")] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                _teacherService.Update(teacher);
                return RedirectToAction(nameof(Index));
            }

            return View(teacher);
        }

        // GET: TeachersController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var teacher = _teacherService.Find(Convert.ToInt32(id));

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: TeachersController/Delete/5
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
