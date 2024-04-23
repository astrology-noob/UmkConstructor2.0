using UmkConstructor.Data.AdditionalModels;

namespace UmkConstructor.Data.ParsingClasses
{
    public class ParsingResult
    {
        // делаю сразу true, потому что нужно отдельно мониторить случаи когда что-то идёт не так
        public bool Successful { get; set; } = true;
        public List<ParsingError> Errors { get; set; } = [];
    }

    public class SimpleParsingResult : ParsingResult
    {
        public string Result { get; set; } = string.Empty;

        public SimpleParsingResult() { }
        public SimpleParsingResult(string data) => Result = data;
    }

    public class SemestersParsingResult : ParsingResult
    {
        public List<SingleSemesterParsingResult> Results { get; set; } = [];
    }

    public class SingleSemesterParsingResult : ParsingResult
    {
        //public SemesterInfoImport Result { get; set; }
        public Dictionary<ParseParams, SimpleParsingResult?> ParsingParams { get; set; } = new()
        {
            { ParseParams.StudyYear, null },
            { ParseParams.SemesterOrder, null },
            { ParseParams.IsAfter11thGrade, null },
            { ParseParams.BusinessRole, null },
            { ParseParams.Hours, null },
            { ParseParams.IndividualWork, null },
            { ParseParams.Weeks, null },
            { ParseParams.EduPractice, null },
            { ParseParams.ProdPractice, null },
            { ParseParams.SessionWeeks, null },
        };
    }
}
