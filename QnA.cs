using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    [Serializable]
    public class QnA
    {
        public string QuestionText { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public List<int> CorrectAnswerIndices { get; set; }

        public QnA()
        {
            PossibleAnswers = new List<string>();
            CorrectAnswerIndices = new List<int>();
        }
    }
}
