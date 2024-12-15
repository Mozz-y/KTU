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
    }
}
