using System.Text.RegularExpressions;

namespace UmkConstructor.Data.AdditionalModels
{
    public readonly struct Code
    {
        public string Name { get; }

        public Code(string name)
        {
            if (Regex.Match(name, @"^\w+\.\d+$").Success)
            {
                Name = name;
            }
        }

        public override string ToString() => Name;

        //public void SetGeneratedCode(Discipline discipline)
        //{

        //}
    }
}
