using Microsoft.AspNetCore.Mvc;
using System;
using UmkConstructor.Data.AdditionalModels;
using UmkConstructor.Data.ParsingClasses;
using UmkConstructor.Services;

namespace UmkConstructor.Controllers
{
    public class FileProcessingController : Controller
    {
        //HttpRequest? request;
        //HttpResponse? response;
        FileParser processor = new();

        private readonly CurriculaService CS;

        // для того чтобы использовать здесь Curricula Service
        public FileProcessingController(CurriculaService cs)
        {
            CS = cs;
        }

        // GET: FileProcessingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FileProcessingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileProcessingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileProcessingController/CreateCurriculumWithFile
        // почему-то переходит не по этому адресу а просто по CreateCurriculumWithFile
        // может вернуть List<ParsingError> с ошибками чтения файла
        // или
        // List<SingleSemesterParsingResult> с семестрами, в парсинге которых возникли критические ошибки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCurriculumWithFile(IFormFileCollection formFiles, string specialty, string academicYear)
        {
            SemestersParsingResult result = processor.ParseCurriculumFile(formFiles);
            
            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            // здесь нужно брать только семестры в которых нет критических ошибок, то есть которые successful = true

            List<SemesterInfoImport> semestersGood = new List<SemesterInfoImport>();
            semestersGood = result.Results.Where(x => x.Successful).Select(x => new SemesterInfoImport(x)).ToList();

            List<SingleSemesterParsingResult> semestersErrors = new List<SingleSemesterParsingResult>();
            semestersGood = result.Results.Where(x => !x.Successful).Select(x => new SemesterInfoImport(x)).ToList();

            // добавить проверку, является файл действительно учебным планом для выбранно специальности
            CurriculumInfoImport newCurriculum = new CurriculumInfoImport()
            {
                Specialty = specialty,
                AcademicYear = academicYear,
                Semesters = semestersGood
            };

            await CS.AddCurriculumFromParsedEntity(newCurriculum);
            
            return Ok(semestersErrors);
        }

        // GET: FileProcessingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileProcessingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: FileProcessingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileProcessingController/Delete/5
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
