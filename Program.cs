using System.Reflection.Metadata;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Quiz Maker!");
            string choice = UIMethods.AskGameMode(); 

            if (choice == Constants.NEW_QUIZ_GAME_MODE)
            {
                List<QnA> questions = UIMethods.CreateQuiz();
                Logic.SaveQuizToXmlFile(questions, Constants.PATH_TO_XML_File);
            }
            else if (choice == Constants.EXISTING_QUIZ_GAME_MODE)
            {
                List<QnA> loadedQuiz = Logic.LoadQuizFromXmlFile(Constants.PATH_TO_XML_File); //part of deserializer I still need to create
                UIMethods.RunQuizOrNotifyIfEmpty(loadedQuiz);

            }
        }
    }
}