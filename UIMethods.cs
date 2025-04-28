using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    public static class UIMethods
    {
        public static QnA CreateQuestionFromUser()
        { 
            QnA question = new QnA();

            Console.WriteLine("Enter a question: ");
            question.QuestionText = Console.ReadLine();
            Console.WriteLine("How many possible answers do you want to add?");
            int answerCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < answerCount; i++)
            {
                Console.Write($"Enter answer option #{i + 1}: ");
                string answer = Console.ReadLine();
                question.PossibleAnswers.Add(answer);//add answer to list
            }

            Console.WriteLine("Now enter the index(es) of the correct answer(s), separated by commas (e.g., 1 or 1,2):");
            string correctAnswers = Console.ReadLine();
            string[] indices = correctAnswers.Split(',');// not a single string, but a collection (array) of strings — hence string[]- Split() returns an array. data type could also be var

            foreach (string index in correctAnswers.Split(','))//index will loop through each one of those correct answer strings
            {//int.TryParse(..) Tries to convert the trimmed string (like "3") into an actual number (like 3).
                //Then the number is stored in a new variable called oneBasedIndex
                if (int.TryParse(index.Trim(), out int oneBasedIndex))//inex.Trim:removes any extra spaces around the number." 3" becomes "3"
                {
                    int zeroBasedIndex = oneBasedIndex - 1;
                    //protects against typos like entering "7" when only 4 options exist.
                    if (zeroBasedIndex >= 0 && zeroBasedIndex < question.PossibleAnswers.Count)
                    question.CorrectAnswerIndices.Add(zeroBasedIndex);
                }
            }
            return question;
        }
    }
}
