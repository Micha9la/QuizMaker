using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    public static class UIMethods
    {        
        public static string AskQuestionText() 
        {
            Console.WriteLine("Enter a question: ");
            return Console.ReadLine();
        }

        public static int AskNumberOfAnswerOptions()
        {
            int count;
            while (true)
            {
                Console.WriteLine("How many answer options?");
                string userInputAnswerOptions = Console.ReadLine();

                if (int.TryParse(userInputAnswerOptions, out count) && count > 0)
                {
                    return count;
                }
                else
                {
                    Console.WriteLine("Please enter a valid positive number.");
                }
            }
        }
        public static List<string> AskAnswerOptionsText(int answerCount)
        {
            List<string> answerOptions = new List<string>();
            for (int i = 1; i <= answerCount; i++)
            {
                Console.Write($"Enter answer option #{i}: ");
                answerOptions.Add(Console.ReadLine());
            }
            return answerOptions;
        }
        public static List<int> AskCorrectAnswerIndices(int totalNumberOfOptions)
        {
            while (true) // loop until valid userInputAnswerOptions
            {
                Console.WriteLine("Enter the correct answer(s) as numbers separated by commas (e.g., 1,3):");
                string userInputIndices = Console.ReadLine();

                var parsedIndices = Logic.ParseCorrectAnswerIndices(userInputIndices, totalNumberOfOptions);

                if (parsedIndices.Count > 0)
                {
                    return parsedIndices; //Return the valid parsed list
                }
                else
                {
                    Console.WriteLine("No valid indices entered. Please try again.\n");
                }
            }

        }
        //Each time BuildSingleQuestionWithAnswers() is called,I get one complete question (with answers and correct indices)
        //packaged into a single QnA object
        public static QnA BuildSingleQuestionWithAnswers()
        {
            string question = AskQuestionText();
            int count = AskNumberOfAnswerOptions();

            List<string> answers = AskAnswerOptionsText(count);
            List<int> correctIndices = AskCorrectAnswerIndices(answers.Count);

            return new QnA
            {
                QuestionText = question,
                PossibleAnswers = answers,
                CorrectAnswerIndices = correctIndices
            };
        }
        public static List<QnA> CreateQuiz()
        {
            List<QnA> quiz = new List<QnA>();

            Console.WriteLine("How many questions do you want to add?");//if 5, user has oportunity to create 5 questions from scratch
            int questionCount = int.Parse(Console.ReadLine());

            for (int i = 1; i <= questionCount; i++)
            {
                Console.WriteLine($"\nCreating question #{i}:");
                QnA qna = BuildSingleQuestionWithAnswers(); //This asks for QuestionText, PossibleAnswers, CorrectAnswerIndices

                quiz.Add(qna);
            }

            return quiz;
        }
    }
}
