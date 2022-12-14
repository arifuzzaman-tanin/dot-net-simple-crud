using Domain.Entites;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Student;

namespace Student.WebMvc.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IAdmittedStudentService _admittedStudentService;

        public StudentsController(IAdmittedStudentService admittedStudentService)
        {
            _admittedStudentService = admittedStudentService;
        }

        // GET: StudentsController
        public ActionResult Index()
        {
            return View(_admittedStudentService.GetAll());
        }

        public ActionResult ByClass()
        {
            return View();
        }

        [HttpGet]
        public JsonResult JsonByClass(string? name = null)
        {
            return Json(_admittedStudentService.ByClass(name));
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsController/Create
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

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("StudentId,Name,Age,Sex,Class,Image,RollNo,ImageFile")] AdmittedStudent student)
        {
            try
            {
                var genders = from Gender sex in Enum.GetValues(typeof(Gender))
                              select new
                              {
                                  GenderId = (int)sex,
                                  Name = sex.ToString()
                              };
                ViewBag.Genders = new SelectList(genders, "GenderId", "Name", student.Sex);

                if (ModelState.IsValid)
                {
                    var isDuplicated = _admittedStudentService.CheckDuplicateRoll(student.RollNo);
                    if (isDuplicated)
                    {
                        ModelState.AddModelError(string.Empty, "Duplicate roll.");
                        return View(student);
                    }

                    _admittedStudentService.Add(student);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(student);
            }

            return View(student);
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var student = _admittedStudentService.Find(Convert.ToInt32(id));

            if (student == null)
            {
                return NotFound();
            }

            var genders = from Gender sex in Enum.GetValues(typeof(Gender))
                          select new
                          {
                              GenderId = (int)sex,
                              Name = sex.ToString()
                          };
            ViewBag.Genders = new SelectList(genders, "GenderId", "Name", student.Sex);

            return View(student);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("StudentId,Name,Age,Sex,Class,Image,RollNo,ImageFile")] AdmittedStudent student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            var genders = from Gender sex in Enum.GetValues(typeof(Gender))
                          select new
                          {
                              GenderId = (int)sex,
                              Name = sex.ToString()
                          };
            ViewBag.Genders = new SelectList(genders, "GenderId", "Name", student.Sex);

            if (ModelState.IsValid)
            {
                var isDuplicated = _admittedStudentService.CheckDuplicateRoll(student.RollNo);
                if (isDuplicated)
                {
                    ModelState.AddModelError(string.Empty, "Duplicate roll.");
                    return View(student);
                }

                _admittedStudentService.Update(student);
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var student = _admittedStudentService.Find(Convert.ToInt32(id));

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            var student = _admittedStudentService.Find(Convert.ToInt32(id));

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                _admittedStudentService.Delete(student);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                return View();
            }
        }
    }
}
