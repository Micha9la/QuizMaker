using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace QuizMaker
{
    public class Logic
    {
        public static void SafeQuizToXmlFile (List<QnA> questions, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<QnA>));
            //use this in main but with a CONSTANT: Logic.SafeQuizToXmlFile(questions, @"C:\Users\maria\Desktop\Programming\Rakete\quiz.xml");
            using (FileStream file = File.Create(path))
            {
            serializer.Serialize(file, questions);
            }
        }
        //It takes the raw input string from the user (e.g. "1,3") and the number of answers to validate against.
        public static List<int> ParseCorrectAnswerIndices(string userInput, int totalNumberOfOptions)//public and static- Ican call it without creating an instance
        {
            List<int> correctAnswerIndices = new List<int>(); //create a new list to store the final, converted, validated answer indices
            foreach (string answerIndexText in userInput.Split(','))//We split the user’s input string by comma.Each piece(e.g. "1", " 3") becomes a loop variable answerIndexText
            {
                if (int.TryParse(answerIndexText.Trim(), out int oneBasedIndex))//" 3" becomes "3" and parse it into int.
                {
                    int zeroBasedIndex = oneBasedIndex - Constants.ZERO_BASED_OFFSET;//"1" becomes 0
                    if (zeroBasedIndex >= 0 && zeroBasedIndex < totalNumberOfOptions)//For 4 answers, valid are 0, 1, 2, 3
                    {
                        correctAnswerIndices.Add(zeroBasedIndex);//If valid, we add it to the final list of correct indices
                    }
                }

            }
            return correctAnswerIndices; //It returns a list of int — the validated correct answer indices (zero-based).
        }
    }

}
        