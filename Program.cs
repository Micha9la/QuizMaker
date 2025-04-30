using System.Reflection.Metadata;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Quiz Maker!");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1 - Create and Save a New Quiz");
            Console.WriteLine("2 - Play Existing Quiz");
            string choice = Console.ReadLine();

            if (choice == Constants.NEW_QUIZ_GAME_MODE)
            {
                List<QnA> questions = UIMethods.CreateQuiz();
                Logic.SaveQuizToXmlFile(questions, Constants.PATH_TO_XML_File);
            }
            else if (choice == Constants.EXISTING_QUIZ_GAME_MODE)
            {
                List<QnA> loadedQuiz = Logic.LoadQuizFromXml(loadPath); //part of deserializer you still need to create
                UIMethods.PlayQuiz(loadedQuiz);
            }
        }
    }
}