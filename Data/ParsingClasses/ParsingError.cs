namespace UmkConstructor.Data.ParsingClasses
{
    public enum ErrorLevel
    {
        Critical,
        Minor
    }

    public class ParsingError
    {
        public string Message { get; set; }

        public ErrorLevel Level { get; set; }

        public ParsingError(string message)
        {
            Message = message;
        }

        public ParsingError(string message, ErrorLevel level)
        {
            Message = message;
            Level = level;
        }

        //public string GetFullErrorMessage()
        //{
        //    return $"{Message} Проверьте, правильно ли оформлен файл.";
        //}
    }

    // добавить типы ошибок??
    // типа, мне это нужно просто чтобы сохранять полезную информацию.. как например название листа на котором возникла ошибка..
}
