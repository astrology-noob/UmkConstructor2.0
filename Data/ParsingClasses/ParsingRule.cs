using OfficeOpenXml;

namespace UmkConstructor.Data.ParsingClasses
{
    public enum ParsingRuleType
    {
        Simple,
        MultipleUntilKeyword
    }

    public enum ParsedDataType
    {
        Int,
        String
    }

    // заменить на билдер?
    public class ParsingRule
    {
        public List<string> Keywords { get; set; }

        public ExcelCellAddress DefaultCellAddress { get; set; }

        // хз нужно ли
        //public ExcelCellAddress PossibleCellRangeToSearch { get; set; }

        public ParsingRuleType RuleType {get; set;}

        // добавить в конструктор
        public ParsedDataType TargetType { get; set;}
        
        public CellToSearchIn CellToSearchIn { get; set; }

        public bool IsRequired { get; set; }

        public ParsingError Error { get; set; }

        public ParsingRule(
            List<string> keywords, 
            string defaultCellAddress,
            bool isRequired = false,
            string errorMessage = "Не получилось найти значение по указанным ключевым словам", 
            ParsingRuleType ruleType = ParsingRuleType.Simple,
            ParsedDataType parsedDataType = ParsedDataType.Int)
        {
            try
            {
                DefaultCellAddress = new ExcelCellAddress(defaultCellAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Keywords = keywords;
            IsRequired = isRequired;

            Error = new ParsingError(errorMessage);
            if (IsRequired)
                Error.Level = ErrorLevel.Critical;
            else
                Error.Level = ErrorLevel.Minor;

            RuleType = ruleType;
            CellToSearchIn = GetCellToSearchIn(ruleType);
        }

        private CellToSearchIn GetCellToSearchIn(ParsingRuleType ruleType) => ruleType switch
        {
            ParsingRuleType.Simple => new CellToSearchIn(),
            ParsingRuleType.MultipleUntilKeyword => new CellToSearchIn(CellDirection.Under),
            _ => throw new ArgumentOutOfRangeException(nameof(ruleType), $"Not expected direction value: {ruleType}"),
        };
    }
}
