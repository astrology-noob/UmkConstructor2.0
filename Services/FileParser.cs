using Microsoft.CodeAnalysis;
using OfficeOpenXml;
using UmkConstructor.Data.ParsingClasses;

namespace UmkConstructor.Services
{
    public class FileParser
    {
        // путь к папке для временного хранения загружаемых файлов
        static string UploadPath = $"{Directory.GetCurrentDirectory()}/tempFileVault";
        static string MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        ExcelPackage? file;
        
        static Dictionary<ParseParams, ParsingRule> rules = new()
        {
            { ParseParams.Hours, new ParsingRule(["Всего аудиторных часов"], "B3", true, "Не получается найти информацию о количестве часов.") },
            { ParseParams.IndividualWork, new ParsingRule(["Самостоятельная работа"], "B4", true, "Не получается найти информацию о количестве часов, отведённом на самостоятельную работу.") },
            { ParseParams.Weeks, new ParsingRule(["Кол-во недель учебных"], "B5", true, "Не получается найти информацию о количестве учебных недель.") },
            { ParseParams.EduPractice, new ParsingRule(["Кол-во недель учебной практики"], "B6", false, "Не получается найти информацию о количестве недель учебной практики.") },
            { ParseParams.ProdPractice, new ParsingRule(["Кол-во недель Производственная практика"], "B7", false, "Не получается найти информацию о количестве недель производственной практики.") },
            { ParseParams.SessionWeeks, new ParsingRule(["Кол-во недель сессии(вычитки)", "Кол-во недель вычитки"], "B8", false, "Не получается найти информацию о количестве недель производственной практики.") },
        };

        public SemestersParsingResult ParseCurriculumFile(IFormFileCollection files)
        {
            SemestersParsingResult semesterParsingResult = new SemestersParsingResult();

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                string fullPath = $"{UploadPath}/{files[0].FileName}";

                // не будет работать, потому что файл не сохраняется по этому пути
                file = new ExcelPackage(fullPath);
                
                var semesterSheets = file.Workbook.Worksheets;

                foreach (ExcelWorksheet semesterSheet in semesterSheets) 
                {
                    SingleSemesterParsingResult parsedSemester = GetSemesterInfoFromName(semesterSheet.Name);

                    parsedSemester = ParseSemesterInfoFromSheet(parsedSemester, semesterSheet);
                    
                    semesterParsingResult.Results.Add(parsedSemester);
                }
            }

            catch (Exception ex)
            {
                // здесь я перехватываю только ошибки при сохранении "файла"
                semesterParsingResult.Errors.Add(new ParsingError($"Не удалось открыть файл. {ex.Message}", ErrorLevel.Critical));
                semesterParsingResult.Successful = false;

            }
            return semesterParsingResult;
        }

        private SingleSemesterParsingResult GetSemesterInfoFromName(string sheetName)
        {
            SingleSemesterParsingResult parsingResult = new();

            var res = sheetName.Split(" ");

            // извлекаем информацию о бизнес-роли из названия
            SimpleParsingResult bRoleParseRes = new();
            parsingResult.ParsingParams.Add(ParseParams.BusinessRole, bRoleParseRes);
            
            if (char.IsLetter(res[2][0]))
                parsingResult.ParsingParams[ParseParams.BusinessRole]!.Result = res[2];

            // не паршу в инты, потому что ParseRes хранит строки
            var numVals = res.Where(x => int.TryParse(x, out _)).Select(x => x).ToList();

            // попытка записать номер курса
            SimpleParsingResult studyYearParseRes = new();
            parsingResult.ParsingParams.Add(ParseParams.StudyYear, studyYearParseRes);
            try
            {
                // можно ли заменить на просто объект studyYearParseRes?
                parsingResult.ParsingParams[ParseParams.StudyYear]!.Result = numVals[0];
            }
            catch
            {
                parsingResult.ParsingParams[ParseParams.StudyYear]!.Errors.Add(new ParsingError("В названии листа не содержится номер курса", ErrorLevel.Critical));
                parsingResult.ParsingParams[ParseParams.StudyYear]!.Successful = false;
                parsingResult.Successful = false;
            }

            // попытка записать номер семестра
            SimpleParsingResult semOrderParseRes = new();
            parsingResult.ParsingParams.Add(ParseParams.SemesterOrder, semOrderParseRes);
            try
            {
                parsingResult.ParsingParams[ParseParams.SemesterOrder]!.Result = numVals[1];
            }
            catch
            {
                parsingResult.ParsingParams[ParseParams.SemesterOrder]!.Errors.Add(new ParsingError("В названии листа не содержится номер семестра", ErrorLevel.Critical));
                parsingResult.ParsingParams[ParseParams.SemesterOrder]!.Successful = false;
                parsingResult.Successful = false;
            }

            // попытка определить после 11-ли класса семестр, рассматривает только семестр в котором явно указан класс
            // в ост. случаях нужно дублировать, кроме 1 курса!!
            SimpleParsingResult isAfter11ParseRes = new();
            parsingResult.ParsingParams.Add(ParseParams.IsAfter11thGrade, isAfter11ParseRes);
            try
            {
                parsingResult.ParsingParams[ParseParams.IsAfter11thGrade]!.Result = (numVals[2] == "11").ToString();
            }
            catch
            {
                parsingResult.ParsingParams[ParseParams.IsAfter11thGrade]!.Errors.Add(new ParsingError("Название листа не содержит информацию о том, пришла ли группа после 11 или 9 класса. Будет принят 9 класс.", ErrorLevel.Minor));
            }

            return parsingResult;
        }

        private SingleSemesterParsingResult ParseSemesterInfoFromSheet(SingleSemesterParsingResult semesterObject, ExcelWorksheet semesterSheet)
        {   
            foreach (var parseParam in semesterObject.ParsingParams.Keys)
            {
                try
                {
                    var res = ApplyParsingRule(semesterSheet, rules[parseParam]);
                    semesterObject.ParsingParams[parseParam] = res;
                    
                    if (!res.Successful)
                        semesterObject.Successful = false;
                }
                catch
                {
                    // сюда попадает когда пытается найти правило для параметра у которого нет правила (оно ищется в имени листа)
                }
            }

            return semesterObject;
        }

        private SimpleParsingResult ApplyParsingRule(ExcelWorksheet sheet, ParsingRule rule)
        {
            SimpleParsingResult parsingResult = new();

            // неправильно получаю значение, нужно искать в соответствующей ячейке из ParsingRule
            foreach (var keyword in rule.Keywords)
            {
                parsingResult.Result =
                    (from cell in sheet.Cells[rule.DefaultCellAddress.Address]
                     where cell.Value?.ToString() == keyword
                     select cell.Value.ToString()).First() ?? string.Empty;

                if (parsingResult.Result == string.Empty)
                {
                    string colLetter = string.Concat(rule.DefaultCellAddress.Address.Where(x => char.IsLetter(x)));

                    // определить границу поиска, потому что он так может долго искать без необходимости
                    parsingResult.Result =
                        (from cell in sheet.Cells[$"{colLetter}:{colLetter}"]
                         where cell.Value?.ToString() == keyword
                         select cell.Value.ToString()).First() ?? string.Empty;

                    if (parsingResult.Result == string.Empty)
                    {
                        if (rule.IsRequired)
                        {
                            parsingResult.Successful = false;
                        }
                        parsingResult.Errors.Add(rule.Error);
                    }
                }

                // проверка на то, получается ли преобразовать в int. Если не получается, то добавляется ошибка, и Successful меняется на false если это было обязательное поле
                if (rule.TargetType == ParsedDataType.Int)
                {
                    if (!int.TryParse(parsingResult.Result, out _))
                    {
                        parsingResult.Errors.Add(new ParsingError("Не удаётся преобразовать полученные данные в числовое значение. Проверьте файл"));
                        
                        if (rule.IsRequired)
                        {
                            parsingResult.Successful = false;
                        }
                    }
                }
            }

            return parsingResult;
        }
    }
}
