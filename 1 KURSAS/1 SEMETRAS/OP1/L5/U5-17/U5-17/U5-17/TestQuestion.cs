using System.Collections.Generic;

namespace U5_17
{
    public class TestQuestion : Question
    {
        public List<string> PossibleAnswers { get; set; }

        public override string GetAdditionalInfo()
        {
            return string.Join(", ", PossibleAnswers); // Join possible answers with semicolons
        }

        public override string ToString()
        {
            return base.ToString(); // Reuse the base class ToString
        }
    }
}
