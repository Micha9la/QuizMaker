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

            if (choice == "1")
            {
                List<QnA> questions = UI.CreateQuiz();  // This method you still need to create to ask user for multiple Qs
                Logic.SaveQuizToXml(questions, savePath);
            }
            else if (choice == "2")
            {
                List<QnA> loadedQuiz = Logic.LoadQuizFromXml(loadPath); //part of deserializer you still need to create
                UI.PlayQuiz(loadedQuiz);
            }
        }
    }
}