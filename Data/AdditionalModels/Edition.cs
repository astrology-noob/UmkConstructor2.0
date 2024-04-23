using System.Text.RegularExpressions;

namespace UmkConstructor.Data.AdditionalModels
{
    public readonly struct Edition
    {
        public string Name { get; }

        // придумать формат для версии + методы для создания версий
        public Edition(string name)
        {
            if (Regex.Match(name, @"^\w+\.\d+$").Success)
            {
                Name = name;
            }
        }

        public override string ToString() => Name;
    }
}
