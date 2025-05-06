using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static int AskNumberOfQuestionsToPlay(int maxAvailable)
        {
            int count;
            while (true)
            {
                Console.Write($"How many questions do you want to answer? (1 to {maxAvailable}): ");
                string input = Console.ReadLine();

                //count will hold the number the user types in the console — how many questions they want to play.
                //It only gets assigned a value if parsing from the input succeeds.
                if (int.TryParse(input, out count) && count >= 1 && count <= maxAvailable)
                    return count;

                Console.WriteLine("Invalid input. Try again.");
            }
        }

        public static bool AskQuestionFromQuiz(QnA question)
        {
            Console.WriteLine(question.QuestionText);

            for (int i = 0; i < question.PossibleAnswers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.PossibleAnswers[i]}");
            }

            Console.Write("Your answer(s): ");
            string input = Console.ReadLine();
            List<int> userAnswers = Logic.ParseCorrectAnswerIndices(input, question.PossibleAnswers.Count);

            //sort both lists so that their order doesn’t matter when comparing.
            //Correct answer: [1, 2], User input: [2, 1]
            //Sorting makes both into[1, 2], so comparison becomes fair
            userAnswers.Sort();
            question.CorrectAnswerIndices.Sort();

            //SequenceEqual compares two lists, item by item, in the same order.
            //If all elements and their order are equal → returns true
            bool isCorrect = userAnswers.SequenceEqual(question.CorrectAnswerIndices);
            Console.WriteLine(isCorrect ? "Correct!\n" : "Incorrect.\n");

            return isCorrect;
        }

        public static void PlayQuiz(List<QnA> quiz)
        {
            if (quiz.Count == 0)
            {
                Console.WriteLine("No questions to play.");
                return;
            }

            int questionCount = AskNumberOfQuestionsToPlay(quiz.Count);
            List<QnA> selectedQuestions = Logic.SelectRandomQuestions(quiz, questionCount);

            int score = 0;
            for (int i = 0; i < selectedQuestions.Count; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1} of {questionCount}");
                if (AskQuestionFromQuiz(selectedQuestions[i]))
                {
                    score++;
                }
            }

            Console.WriteLine($"\n You scored {score} out of {questionCount}.");
        }

    }
}
